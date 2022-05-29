using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static LearnWebsite.Core.DTOs.UserPanelViewModel;

namespace LearnWebsite.Web.Pages.Admin.User
{
    public class DeleteUserModel : PageModel
    {
        IUserService _userServicve;

        public DeleteUserModel(IUserService userServicve)
        {
            _userServicve = userServicve;
        }

        public UserInformationViewModel UserInformationViewModel { get; set; }
        public void OnGet(int userId)
        {
            ViewData["UserId"] = userId;
            UserInformationViewModel = _userServicve.GetUserInformation(userId);
        }

        public IActionResult OnPost(int userId)
        {
            _userServicve.DeleteUserInAdmin(userId);
            return RedirectToAction("Index");
        }
    }
}
