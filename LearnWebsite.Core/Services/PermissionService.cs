using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Data.Contexts;
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

        public PermissionService(LearnWebsiteContext learnWebsiteContext)
        {
            _context = learnWebsiteContext;
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

        public List<Role> GetAllRole()
        {
            return _context.Roles.ToList();
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
    }
}
