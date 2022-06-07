using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Security;

namespace LearnWebsite.Web.Pages.Admin.User
{
    [PermissionChecker(2)]
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
