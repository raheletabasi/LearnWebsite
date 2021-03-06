using LearnWebsite.Data.Entities.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.DTOs
{
    public class ManagementUserViewModel
    {
        public List<User> Users { get; set; }

        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class CreateUserViewModel
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

        [Display(Name = "کلمه عبور")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
        public string Password { get; set; }

        [Display(Name = "آواتار")]
        public IFormFile Avatar { get; set; }

        public List<int> SelectedRoles { get; set; }
    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
        public string Password { get; set; }

        [Display(Name = "وضعیت فعال بودن")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        public IFormFile Avatar { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string UserAvatar { get; set; }

        public List<int> Roles { get; set; }
    }
}
