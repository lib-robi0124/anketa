using Microsoft.AspNetCore.Mvc;

namespace GlasAnketa.Controllers
{
    public class ManagerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewResults()
        {
            // Use the shared Reports dashboard
            return RedirectToAction("Index", "Reports");
        }
    }
}