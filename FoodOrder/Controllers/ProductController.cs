using Microsoft.AspNetCore.Mvc;

namespace FoodOrderUI.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
