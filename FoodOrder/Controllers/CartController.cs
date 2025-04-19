using Microsoft.AspNetCore.Mvc;

namespace FoodOrderUI.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
