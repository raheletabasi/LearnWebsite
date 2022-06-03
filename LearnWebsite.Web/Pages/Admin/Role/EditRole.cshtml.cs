using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public void OnGet(int RoleId)
        {
            CurrentRole = _permissionService.GetRoleByRoleId(RoleId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _permissionService.UpdateRole(CurrentRole);

            return RedirectToPage("Index");
        }
    }
}
