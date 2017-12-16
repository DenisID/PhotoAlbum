using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Client.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20)]
        [RegularExpression(@"[A-Za-z0-9_-]*", ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.ValErrLoginRegExp))]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.Login))]
        public string Login { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.Password))]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(20)]
        [RegularExpression(@"[A-Za-z0-9_-]*", ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.UnacceptableLogin))]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.Login))]
        public string Login { get; set; }

        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.UnacceptableEmail))]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.Email))]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.FirstName))]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.LastName))]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.Password))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.ConfirmPassword))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.PasswordsDoNotMatch))]
        public string ConfirmPassword { get; set; }
    }

    public class EditUserProfileViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.UnacceptableEmail))]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.Email))]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.FirstName))]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.LastName))]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.OldPassword))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.NewPassword))]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.ConfirmPassword))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.PasswordsDoNotMatch))]
        public string ConfirmPassword { get; set; }
    }

    public class EditUserInfoViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.UnacceptableEmail))]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.Email))]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.FirstName))]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.LastName))]
        public string LastName { get; set; }
    }

    public class EditUserPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.OldPassword))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.NewPassword))]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.ResourceEN), Name = nameof(Resources.ResourceEN.ConfirmPassword))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.ResourceEN), ErrorMessageResourceName = nameof(Resources.ResourceEN.PasswordsDoNotMatch))]
        public string ConfirmPassword { get; set; }
    }
}
