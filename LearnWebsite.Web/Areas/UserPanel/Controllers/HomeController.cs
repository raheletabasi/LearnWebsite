using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace LearnWebsite.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }

        #region EditProfile
        [Route("/UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetInfoForEdit(User.Identity.Name));
        }

        [HttpPost]
        [Route("/UserPanel/EditProfile")]
        public IActionResult EditProfile(UserPanelViewModel.EditProfileViewModel editProfileViewModel)
        {
            if (!ModelState.IsValid)
                return View(editProfileViewModel);

            if (_userService.CheckDuplicateEmail(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()), editProfileViewModel.Email))
            {
                ModelState.AddModelError("Email", $"ایمیل {editProfileViewModel.Email} در سامانه توسط شخص دیگری ثبت شده است");
                return View(editProfileViewModel);
            }
            if (_userService.CheckDuplicateUser(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()), editProfileViewModel.UserName))
            {
                ModelState.AddModelError("UserName", $"نام کاربری {editProfileViewModel.Email} در سامانه توسط شخص دیگری ثبت شده است");
                return View(editProfileViewModel);
            }

            _userService.UpdateProfilePanel(User.Identity.Name, editProfileViewModel);

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.IsSuccess = true;
            return Redirect("/Login");
        }

        [Route("/UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("/UserPanel/ChangePassword")]
        public IActionResult ChangePassword(UserPanelViewModel.ChangePasswordViewModel changePasswordViewModel)
        {
            string currentUser = User.Identity.Name;

            if(!ModelState.IsValid)
                return View(changePasswordViewModel);

            if (!_userService.GetPassword(currentUser, changePasswordViewModel.OldPassword))
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی اشتباه می باشد");

            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion
    }
}
