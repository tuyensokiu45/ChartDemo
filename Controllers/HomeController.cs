using Microsoft.AspNetCore.Mvc;

namespace ChartDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
