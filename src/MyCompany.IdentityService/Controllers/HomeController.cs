using Microsoft.AspNetCore.Mvc;

namespace MyCompany.IdentityService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}