using LearnWebsite.Data.Entities.Permission;
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
        #region Role
        public List<Role> GetAllRole();

        public void AddRoleToUser(List<int> roleId, int userId);

        public void UpdateUserRole(List<int> roleId, int userId);

        public int CreateRole(Role role);

        public Role GetRoleByRoleId(int roleId);

        public void UpdateRole(Role role);

        public void DeleteRole(int roleId);
        #endregion

        #region Permission
        List<Permission> GetAllPermission();
        List<int> GetPermissionByRoleId(int roleId);
        void AddRolePermission(int roleId, List<int> permissions);
        void UpdateRolePermission(int roleId, List<int> permissions);

        #endregion
    }
}
