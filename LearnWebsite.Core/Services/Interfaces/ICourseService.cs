using LearnWebsite.Data.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWebsite.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region CourseGroup
        List<CourseGroup> GetAllCourseGroups();
        #endregion
    }
}
