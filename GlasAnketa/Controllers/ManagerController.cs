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

        // --- Management shortcuts for Manager ---
        // Managers can navigate to Admin management screens but with limited capabilities:
        // - Manage Questions: full access (same as Admin)
        // - Manage Forms: full access (same as Admin)
        // - Manage Users: only "Create User" (no edit/deactivate/reset)

        public async Task<IActionResult> ManageQuestions()
        {
            // Redirect to Admin's ManageQuestions
            return RedirectToAction("ManageQuestions", "Admin");
        }

        public async Task<IActionResult> ManageForms()
        {
            // Redirect to Admin's ManageForms
            return RedirectToAction("ManageForms", "Admin");
        }

        public async Task<IActionResult> ManageUsers()
        {
            // Managers are only allowed to add new users, so redirect directly to CreateUser.
            return RedirectToAction("CreateUser", "Admin");
        }
    }
}
