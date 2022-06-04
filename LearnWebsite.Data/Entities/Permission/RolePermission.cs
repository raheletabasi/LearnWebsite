using LearnWebsite.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Data.Entities.Permission
{
    public class RolePermission
    {
        public RolePermission()
        {

        }

        [Key]
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }


        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
