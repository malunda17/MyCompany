using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyCompany.Common.Logger;
using MyCompany.IdentityService.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.IdentityService.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IRabbitLogger _rabbitLogger;

        public AccountController(ILogger<AccountController> logger, IRabbitLogger rabbitLogger, UserManager<User> userManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rabbitLogger = rabbitLogger ?? throw new ArgumentNullException(nameof(rabbitLogger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users.Select(u => new UserViewModel
            {
                Login = u.UserName,
                Name = u.Name,
                Surname = u.Surname
            })); ;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new User
            {
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.Login,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                await _rabbitLogger.LogAsync(new LogMessage(0, "userName", "CreateUser"));
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Home()
        {
            ViewData["UserName"] = HttpContext.User.Identity.Name;
            return View();
        }
    }
}