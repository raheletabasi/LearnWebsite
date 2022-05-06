using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebsite.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]    
    public class CashWalletController : Controller
    {
        IUserService _userService;

        public CashWalletController(IUserService userService) => _userService = userService;

        [Route("UserPanel/CashWallet")]
        public IActionResult CashWallet()
        {
            return View();
        }

        [HttpPost]
        [Route("UserPanel/CashWallet")]
        public IActionResult CashWallet(ChargeCashWalletViewModel chargeCashWalletViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ListOfCharge"] = _userService.GetHistoryCashWallet(User.Identity.Name);
                return View(chargeCashWalletViewModel);
            }

            _userService.ChargeCashWallet(User.Identity.Name, chargeCashWalletViewModel.Amount);

            return View();
        }
    }
}
