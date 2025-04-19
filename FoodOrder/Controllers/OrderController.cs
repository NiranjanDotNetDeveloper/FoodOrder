using Microsoft.AspNetCore.Mvc;

namespace FoodOrderUI.Controllers
{
    [Route("[controller]")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
