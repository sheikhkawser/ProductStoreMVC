using Microsoft.AspNetCore.Mvc;
using ProductStore.DBContext;

namespace ProductStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductStoreDBContext _context;

        public HomeController(ProductStoreDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
