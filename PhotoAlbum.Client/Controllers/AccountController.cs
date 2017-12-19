using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PhotoAlbum.Client.Models;
using PhotoAlbum.Client.BusinessServices.Interfaces;
using AutoMapper;
using PhotoAlbum.Client.Dto;
using System.Net;

namespace PhotoAlbum.Client.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var getTokenDto = Mapper.Map<GetTokenDto>(model);

            var token = await _userService.GetTokenAsync(getTokenDto);
            if(token.AccessToken == null)
            {
                ModelState.AddModelError(String.Empty, Resources.ResourceEN.InvalidLoginOrPassword);

                return View(model);
            }

            AuthenticationProperties options = new AuthenticationProperties();

            options.AllowRefresh = true;
            options.IsPersistent = true;
            options.ExpiresUtc = DateTime.UtcNow.AddSeconds(token.ExpiresIn);

            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, model.Login),
                    new Claim("AcessToken", string.Format(token.AccessToken)),
                };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");

            Request.GetOwinContext().Authentication.SignIn(options, identity);
            
            return RedirectToAction("Index", "Photo");
        }
        
        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerUserDto = Mapper.Map<RegisterUserDto>(model);
            var result = await _userService.RegisterUser(registerUserDto);

            if (!result.Successeded)
            {
                foreach(var errorMsg in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, errorMsg);
                }

                return View(model);
            }

            var loginModel = Mapper.Map<LoginViewModel>(model);
            await Login(loginModel, null);
             
            return RedirectToAction("Index", "Photo");
        }
        
        public async Task<ActionResult> EditUserProfile()
        {
            ViewBag.Result = "";

            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            var dto = await _userService.GetUserProfileAsync(token);

            var model = Mapper.Map<EditUserProfileViewModel>(dto);
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserProfile(EditUserInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var badModel = Mapper.Map<EditUserProfileViewModel>(model);
                ViewBag.ProfileResult = Resources.ResourceEN.Error;

                return View(badModel);
            }

            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            var dto = Mapper.Map<EditUserProfileDto>(model);

            var result = await _userService.EditUserProfileAsync(dto, token);
            if(result == HttpStatusCode.OK)
            {
                ViewBag.ProfileResult = Resources.ResourceEN.ProfileInformationChangedSuccessfully;
            }
            else
            {
                ViewBag.ProfileResult = Resources.ResourceEN.Error;
            }

            dto = await _userService.GetUserProfileAsync(token);

            var newModel = Mapper.Map<EditUserProfileViewModel>(dto);

            return View(newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(EditUserPasswordViewModel model)
        {
            var token = ((ClaimsPrincipal)HttpContext.User).FindFirst("AcessToken").Value;

            if (!ModelState.IsValid)
            {
                var editUserProfileDtoForBadModel = await _userService.GetUserProfileAsync(token);
                var badModel = Mapper.Map<EditUserProfileViewModel>(editUserProfileDtoForBadModel);
                ViewBag.PasswordResult = Resources.ResourceEN.Error;

                return View("EditUserProfile", badModel);
            }

            var changePasswordDto = Mapper.Map<ChangePasswordDto>(model);

            var result = await _userService.ChangePasswordAsync(changePasswordDto, token);
            if (result == HttpStatusCode.OK)
            {
                ViewBag.PasswordResult = Resources.ResourceEN.PasswordChangedSuccessfully;
            }
            else
            {
                ViewBag.PasswordResult = Resources.ResourceEN.Error;
            }

            var editUserProfileDto = await _userService.GetUserProfileAsync(token);

            var newModel = Mapper.Map<EditUserProfileViewModel>(editUserProfileDto);

            return View("EditUserProfile", newModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Photo");
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}