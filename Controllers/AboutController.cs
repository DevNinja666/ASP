using Microsoft.AspNetCore.Mvc;
namespace PortfolioMVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
