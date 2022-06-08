using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Data.Entities.Course
{
    public class CourseGroup
    {
        public CourseGroup()
        {

        }

        [Key]
        public int CourseGroupId { get; set; }

        [Display(Name = "عنوان گروه دوره")]
        [Required(ErrorMessage = "تکمیل نمودن فیلد {0} الزامی می باشد")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string CourseGroupTitle { get; set; }

        public bool IsDelete { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public List<CourseGroup> CourseGroups { get; set; }
    }
}
