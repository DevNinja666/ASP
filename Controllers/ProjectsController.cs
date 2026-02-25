using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace PortfolioMVC.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
