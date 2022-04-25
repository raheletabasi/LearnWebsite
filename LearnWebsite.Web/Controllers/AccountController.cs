using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Services;
using LearnWebsite.Core.Convertor;
using Microsoft.AspNetCore.Mvc;
using LearnWebsite.Core.Services.Interfaces;

namespace LearnWebsite.Web.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if(!ModelState.IsValid)
                return View(registerViewModel);

            if (_userService.IsExistEmail(FixedText.FixEmail(registerViewModel.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده قبلا در سامانه ثبت شده است");
                return View(registerViewModel);
            }

            if (_userService.IsExistUserName(registerViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده قبلا در سامانه ثبت شده است");
                return View(registerViewModel);
            }

            //TODO : Register User

            return RedirectToAction("Index");
        }
    }
}
