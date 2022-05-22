using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnWebsite.Core.DTOs;

namespace LearnWebsite.Web.Pages.Admin.User
{
    public class IndexModel : PageModel
    {
        IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public ManagementUserViewModel managementUserViewModel { get; set; }

        public void OnGet(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            managementUserViewModel = _userService.GetUser(pageId, filterEmail, filterUserName);
        }
    }
}
