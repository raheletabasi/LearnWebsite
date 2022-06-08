using LearnWebsite.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearnWebsite.Web.ViewComponent
{

    public class CourseGroupViewComponent
    {
        ICourseService _courseService;

        public CourseGroupViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)view("CourseGroup", _courseService.GetAllCourseGroups()));
        }
    }
}
