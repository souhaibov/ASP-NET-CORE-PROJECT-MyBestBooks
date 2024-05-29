using Microsoft.AspNetCore.Mvc;

namespace MyBestBooks.Controllers
{
    public class Product : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
