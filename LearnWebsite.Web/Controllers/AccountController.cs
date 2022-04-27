using LearnWebsite.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Data.Entities.User;
using LearnWebsite.Core.Utility.Convertor;
using LearnWebsite.Core.Utility.Generator;
using LearnWebsite.Core.Security;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using LearnWebsite.Core.Utility.Sender;

namespace LearnWebsite.Web.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        IViewRenderService _viewRenderService;

        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }

        #region Register
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

                var user = new User()
                {
                    ActivateCode = Generator.CodeGenerator(),
                    Email = FixedText.FixEmail(registerViewModel.Email),
                    IsActive = false,
                    Password = PasswordHelper.EncodePasswordMd5(registerViewModel.Password),
                    RegisterDate = System.DateTime.Now,
                    UserAvatar = "Default.jpg",
                    UserName = registerViewModel.UserName
                };

                _userService.AddUser(user);

            #region Send Activation Code Email
            string body = _viewRenderService.RenderToStringAsync("ActivationCodeEmail", user);
            SendEmail.Send(user.Email,"فعال سازی حساب کاربری",body);
            #endregion

            return View("SuccessRegister",user);
            }
        #endregion

        #region login
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var userlogin = _userService.LoginUser(loginViewModel);
            if (userlogin != null)
            {
                if (userlogin.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, userlogin.UserId.ToString()),
                        new Claim(ClaimTypes.Name, userlogin.UserName)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = loginViewModel.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                    return View(loginViewModel);
                }
            }
            ModelState.AddModelError("Email", $"ایمیل {loginViewModel.Email} در سامانه ثبت نشد است");
            return View(loginViewModel);
        }
        #endregion

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("/Login");
        }

        #region Active Account
        public IActionResult AccountActivation(string id)
        {
            ViewBag.IsActive = _userService.AccountActivation(id);
            return View();
        }
        #endregion
    }
}
