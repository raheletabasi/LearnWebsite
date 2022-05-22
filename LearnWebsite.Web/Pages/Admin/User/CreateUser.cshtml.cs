using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LearnWebsite.Web.Pages.Admin.User
{
    public class CreateUserModel : PageModel
    {
        IUserService _userService;
        IPermissionService _permissionService;

        public CreateUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetAllRole();
        }

        public IActionResult OpPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            int userId = _userService.AddUserInAdmin(CreateUserViewModel);

            _permissionService.AddRoleToUser(SelectedRoles, userId);

            return Redirect("/admin/user");
        }
    }
}
