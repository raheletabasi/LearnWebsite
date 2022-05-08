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

            int walletId = _userService.ChargeCashWallet(User.Identity.Name, chargeCashWalletViewModel.Amount);

            #region Online Payment

            var payment = new ZarinpalSandbox.Payment((int)chargeCashWalletViewModel.Amount);

            var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44349/OnlinePayment/" + walletId, "Info@topLearn.Com", "09197070750");

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            #endregion

            return View();
        }
    }
}
