using LearnWebsite.Core.Security;
using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LearnWebsite.Web.Pages.Admin.Role
{
    [PermissionChecker(7)]
    public class CreateRoleModel : PageModel
    {
        IPermissionService _permissionService;

        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Data.Entities.User.Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permission"] = _permissionService.GetAllPermission();
        }
      
        public IActionResult OnPost(List<int> selectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();

            Role.IsDelete = false;
            int roleId = _permissionService.CreateRole(Role);

            _permissionService.AddRolePermission(roleId, selectedPermission);

            return RedirectToPage("Index");
        }
    }
}
