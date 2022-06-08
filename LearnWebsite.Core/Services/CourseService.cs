using LearnWebsite.Core.Services.Interfaces;
using LearnWebsite.Data.Contexts;
using LearnWebsite.Data.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnWebsite.Data.Entities.Course;

namespace LearnWebsite.Core.Services
{
    public class CourseService : ICourseService
    {
        LearnWebsiteContext _context;
        ICourseService _courseService;

        public CourseService(LearnWebsiteContext context)
        {
            _context = context;
        }

        public List<CourseGroup> GetAllCourseGroups()
        {
            return _context.CourseGroups.ToList();
        }
    }
}
