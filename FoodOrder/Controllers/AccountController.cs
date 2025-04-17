using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Controllers
{
    [Route("[controller]/[action]")]
    [Route("/")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
