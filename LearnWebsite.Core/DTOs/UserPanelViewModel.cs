using LearnWebsite.Core.Utility.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LearnWebsite.Core.DTOs
{
    public class UserPanelViewModel
    {
        public class UserInformationViewModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string RegisterDate { get; set; }
            public int CashWallet { get; set; }
        }

        public class SideBarViewModel
        {
            public string UserName { get; set; }
            public string RegisteDate { get; set; }
            public string Avatar { get; set; }
        }

        public class EditProfileViewModel
        {
            [Display(Name = "نام کاربری")]
            [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
            [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
            public string UserName { get; set; }

            [Display(Name = "ایمیل")]
            [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
            [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
            [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
            public string Email { get; set; }

            [AllowedExtensions(new string[] { ".jpg", ".png"})]
            public IFormFile AvatarUploaded { get; set; }

            public string AvatarName { get; set; }
        }

        public class ChangePassword
        {
            [Display(Name = "کلمه عبور فعلی")]
            [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
            [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
            [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
            public string OldPassword { get; set; }

            [Display(Name = "کلمه عبور")]
            [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
            [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
            [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
            public string Password { get; set; }

            [Display(Name = "تکرار کلمه عبور")]
            [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
            [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
            [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
            [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمی باشد")]
            public string RePassword { get; set; }
        }
    }
}
