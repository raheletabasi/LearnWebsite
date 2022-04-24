using Microsoft.AspNetCore.Mvc;

namespace LearnWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
