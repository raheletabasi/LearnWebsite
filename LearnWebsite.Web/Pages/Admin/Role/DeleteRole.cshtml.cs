using LearnWebsite.Core.Security;
using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnWebsite.Web.Pages.Admin.Role
{
    [PermissionChecker(8)]
    public class DeleteRoleModel : PageModel
    {
        IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService)
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

        public IActionResult OnPost(int roleId)
        {
            _permissionService.DeleteRole(roleId);

            return RedirectToPage("Index");
        }
    }
}
