using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمی باشد")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public class ForgetpasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }

        [Display(Name = "کلمه عبور")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تمکیل نمودن فیلد {0} الزامی می باشد")]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور یکسان نمی باشد")]
        public string RePassword { get; set; }
    }
}
