using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnWebsite.Web.Pages.Admin.Role
{
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

        }
      
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Role.IsDelete = false;
            int roleId = _permissionService.CreateRole(Role);

            //TODO Add Permission

            return RedirectToPage("Index");
        }
    }
}
