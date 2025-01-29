using Microsoft.AspNetCore.Mvc;

namespace BankingApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
