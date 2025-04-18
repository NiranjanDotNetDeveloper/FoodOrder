using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [Route("register")]
        [Route("/")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
