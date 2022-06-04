using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Data.Entities.Permission
{
    public class Permission
    {
        public Permission()
        {

        }

        [Key]
        public int PermissionId { get; set; }

        [Display(Name = "عنوان دسترسی")]
        [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
        [MaxLength(150,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string PermissionTitle { get; set; }

        public int? ParentId { get; set; }


        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}
