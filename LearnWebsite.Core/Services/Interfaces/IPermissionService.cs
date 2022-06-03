using LearnWebsite.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        public List<Role> GetAllRole();

        public void AddRoleToUser(List<int> roleId, int userId);

        public void UpdateUserRole(List<int> roleId, int userId);

        public int CreateRole(Role role);

        public Role GetRoleByRoleId(int roleId);

        public void UpdateRole(Role role);
    }
}
