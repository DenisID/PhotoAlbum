using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoAlbum.Client.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20)]
        [RegularExpression(@"[A-Za-z0-9_-]*", ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.ValErrLoginRegExp))]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.Login))]
        public string Login { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.Password))]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(20)]
        [RegularExpression(@"[A-Za-z0-9_-]*", ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.UnacceptableLogin))]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.Login))]
        public string Login { get; set; }

        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.UnacceptableEmail))]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.Email))]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.FirstName))]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.LastName))]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.Password))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.ConfirmPassword))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.PasswordsDoNotMatch))]
        public string ConfirmPassword { get; set; }
    }

    public class EditUserProfileViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.UnacceptableEmail))]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.Email))]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.FirstName))]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.LastName))]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.OldPassword))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.NewPassword))]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.ConfirmPassword))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.PasswordsDoNotMatch))]
        public string ConfirmPassword { get; set; }
    }

    public class EditUserInfoViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.UnacceptableEmail))]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.Email))]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.FirstName))]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.LastName))]
        public string LastName { get; set; }
    }

    public class EditUserPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.OldPassword))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.PasswordToShortOrToLong))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.NewPassword))]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources.Localization), Name = nameof(Resources.Localization.ConfirmPassword))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources.Localization), ErrorMessageResourceName = nameof(Resources.Localization.PasswordsDoNotMatch))]
        public string ConfirmPassword { get; set; }
    }
}
