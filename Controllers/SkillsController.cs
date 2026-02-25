using Microsoft.AspNetCore.Mvc;
namespace PortfolioMVC.Controllers
{
    public class SkillsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
