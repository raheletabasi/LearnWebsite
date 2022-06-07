using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Data.Contexts;
using LearnWebsite.Data.Entities.Permission;
using LearnWebsite.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.Services
{
    public class PermissionService : IPermissionService
    {
        LearnWebsiteContext _context;
        IUserService _userService;

        public PermissionService(LearnWebsiteContext learnWebsiteContext, IUserService userService)
        {
            _context = learnWebsiteContext;
            _userService = userService;
        }

        public void AddRolePermission(int roleId, List<int> permissions)
        {
            foreach (var item in permissions)
                _context.RolePermissions.Add(
                    new RolePermission()
                    {
                        RoleId = roleId,
                        PermissionId =  item
                    });

            _context.SaveChanges();
        }

        public void AddRoleToUser(List<int> roleId, int userId)
        {
            foreach (var roleItem in roleId)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleItem,
                    UserId = userId
                });
            }
            _context.SaveChanges();
        }

        public int CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void DeleteRole(int roleId)
        {
            Role deletedRole = GetRoleByRoleId(roleId);
            deletedRole.IsDelete = true;

            _context.Roles.Remove(deletedRole);
            _context.SaveChanges();
        }

        public void UpdateRolePermission(int roleId, List<int> newRolePermission)
        {
            _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToList()
                .ForEach(removeRole => _context.RolePermissions.Remove(removeRole));

            AddRolePermission(roleId, newRolePermission);
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permissions.ToList();
        }

        public List<Role> GetAllRole()
        {
            return _context.Roles.ToList();
        }

        public List<int> GetPermissionByRoleId(int roleId)
        {
            return _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToList();
        }

        public Role GetRoleByRoleId(int roleId)
        {
            return _context.Roles.Find(roleId);           
        }

        public void UpdateRole(Role role)
        {
            Role updateRole = GetRoleByRoleId(role.RoleId);
            updateRole.RoleTitle = role.RoleTitle;

            _context.Roles.Update(updateRole);
            _context.SaveChanges();
        }

        public void UpdateUserRole(List<int> roleId, int userId)
        {
            _context.UserRoles.Where(rol => rol.UserId == userId).ToList().ForEach(removeRole => _context.UserRoles.Remove(removeRole));

            AddRoleToUser(roleId, userId);
        }

        public bool CheckUserPermission(int permissionId, string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            List<int> userRoles = _context.UserRoles
                                    .Where(ur => ur.UserId == userId)
                                    .Select(ur => ur.RoleId)
                                    .ToList();

            if (!userRoles.Any())
                return false;

            List<int> RolePermissions = _context.RolePermissions
                                          .Where(ur => ur.PermissionId == permissionId)
                                          .Select(ur => ur.RoleId)
                                          .ToList();

            return RolePermissions.Any(rp => userRoles.Contains(rp));

        }
    }
}
