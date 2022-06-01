using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace LearnWebsite.Web.Pages.Admin.Role
{
    public class IndexModel : PageModel
    {
        IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Data.Entities.User.Role> roles { get; set; }
        public void OnGet()
        {
            roles = _permissionService.GetAllRole();
        }
    }
}
