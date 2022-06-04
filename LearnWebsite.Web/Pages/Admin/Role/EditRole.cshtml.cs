using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LearnWebsite.Web.Pages.Admin.Role
{
    public class EditRoleModel : PageModel
    {
        IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Data.Entities.User.Role CurrentRole { get; set; }
        public void OnGet(int roleId)
        {
            ViewData["Permission"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermissionFromDB"] = _permissionService.GetPermissionByRoleId(roleId);
            CurrentRole = _permissionService.GetRoleByRoleId(roleId);
        }

        public IActionResult OnPost(int roleId, List<int> selectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();

            _permissionService.UpdateRole(CurrentRole);
            _permissionService.UpdateRolePermission(roleId, selectedPermission);

            return RedirectToPage("Index");
        }
    }
}
