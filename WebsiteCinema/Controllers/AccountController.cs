using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebsiteCinema.Data;
using WebsiteCinema.Data.Static;
using WebsiteCinema.Models;
using WebsiteCinema.ViewModels;

namespace WebsiteCinema.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet("/Login")]
        [HttpGet("/Account/Login")]
        public IActionResult Login() 
        {
            return View(new VMLogin());
        }

        [HttpPost("/Login")]
        [HttpPost("/Account/Login")]
        public async Task<IActionResult> Login(VMLogin login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user != null)
            {
                var password = await _userManager.CheckPasswordAsync(user, login.Password);
                if (password)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
            }

            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(login);
        }

        [HttpGet("/Register")]
        public IActionResult Register()
        {
            return View(new VMRegister());
        }


        [HttpPost("/Register")]
        public async Task<IActionResult> Register(VMRegister register)
        {
            if (!ModelState.IsValid) return View(register);

            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use.";
                return View(register);
            }

            var newUser = new ApplicationUser()
            { 
                UserName = register.Username,
                FullName = register.FullName,
                Email = register.Email,
            };

            var newUserResponce = await _userManager.CreateAsync(newUser, register.Password);
            if (newUserResponce.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegisterCompleted");
            }
            else
            {
                TempData["Error"] = "Credentials error. Email must be xxxx@yyyyy.zzz, Password must contain letters (L,s), symbols (?,&,...), numbers";
                return View(register);
            }
        }

        [HttpPost("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View(ReturnUrl);
        }
    }
}
