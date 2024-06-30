using Microsoft.AspNetCore.Mvc;
using ProductStore.DBContext;

namespace ProductStore.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
