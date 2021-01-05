using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCompany.IdentityService.Models;
using System;
using System.Threading.Tasks;

namespace MyCompany.IdentityService.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<User> _signInManager;

        public AuthController(ILogger<AuthController> logger, SignInManager<User> signInManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult Login([FromQuery]string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var result = await _signInManager.PasswordSignInAsync(form.Username, form.Password, true, false);

            if (result.Succeeded)
            {
                if (form.ReturnUrl == null)
                {
                    return RedirectToAction("Home", "Account");
                }

                return Redirect(form.ReturnUrl);
            }

            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

    }
}