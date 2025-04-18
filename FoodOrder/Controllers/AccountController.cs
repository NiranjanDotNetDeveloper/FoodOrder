using FoodInfrastructure.DbContextClass;
using FoodOrderCoreProject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodOrder.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationDbContext applicationDbContext, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [Route("register")]
        [Route("/")]
        [HttpGet]
        public IActionResult Register()
        {
            RegisterDTO register = new RegisterDTO();
            return View(register);
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            List<string> errorList = new List<string>();
            if (!ModelState.IsValid)
            {
                errorList = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return View(errorList);
            }
            else
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = register.Name,
                    Email = register.Email,
                    PhoneNumber = register.Contact.ToString()
                };
                var newUser = await _userManager.CreateAsync(user, register.Password);
                if (newUser.Succeeded)
                {
                    ApplicationRole role = new ApplicationRole()
                    {
                        Name = register.Role
                    };
                    var isRolePresent = await _roleManager.FindByNameAsync(register.Role);
                    if (isRolePresent == null)
                    {
                        await _roleManager.CreateAsync(role);

                    }
                    await _userManager.AddToRoleAsync(user, register.Role);
                }
            }
            return RedirectToAction("Login");
        }
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            LoginDTO login = new LoginDTO();
            return View(login);
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            List<string> errorList = new List<string>();
            if (!ModelState.IsValid)
            {
                errorList = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return View(errorList);
            }
            else
            {
                var user = await _userManager.FindByNameAsync(login.Name);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Content("Logged in", "text/plain");
                }
                else
                {
                    return RedirectToAction("Register");
                }
            }
        }
        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
