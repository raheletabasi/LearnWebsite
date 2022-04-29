using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Data.Entities.User
{
    public class Role
    {
        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name ="عنوان نقش")]
        [MaxLength(200,ErrorMessage ="{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
        public string RoleTitle { get; set; }


        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
