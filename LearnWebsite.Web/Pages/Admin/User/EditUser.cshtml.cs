using LearnWebsite.Core.DTOs;
using LearnWebsite.Core.Services;
using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LearnWebsite.Web.Pages.Admin.User
{
    public class EditUserModel : PageModel
    {
        IUserService _userService;
        IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        public EditUserViewModel editUserViewModel { get; set; }
        public void OnGet(int userId)
        {
            editUserViewModel = _userService.GetUserInfoInAdmin(userId);
            ViewData["Roles"] = _permissionService.GetAllRole();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
                return Page();

            _userService.EditUserInAdmin(editUserViewModel);
            _permissionService.UpdateUserRole(editUserViewModel.Roles, editUserViewModel.UserId);

            return Redirect("/Admin/User");
        }
    }
}
