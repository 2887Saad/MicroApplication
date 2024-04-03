using Microsoft.AspNetCore.Mvc;

namespace Micro.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
         public IActionResult Update()
        {
            return View();
        }
    }
}
