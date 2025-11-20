using GlasAnketa.Services.Interfaces;
using GlasAnketa.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using GlasAnketa.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace GlasAnketa.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IQuestionFormService _formService;
        private readonly AppDbContext _context;

        public AccountController(IUserService userService, IQuestionFormService formService, AppDbContext context)
        {
            _userService = userService;
            _formService = formService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserCredentialsVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // First try to login as regular User
            var user = await _userService.ValidateUser(model);
            
            if (user != null)
            {
                // Regular user login - Store user in session
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserRole", user.Role.Name);
                HttpContext.Session.SetInt32("RoleId", user.Role.RoleId);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetInt32("CompanyId", user.CompanyId);
                HttpContext.Session.SetString("LastActivity", DateTime.UtcNow.ToString());

                if (user.Role.Name == "Administrator")
                    return RedirectToAction("Index", "Admin");
                if (user.Role.RoleId == 3 || string.Equals(user.Role.Name, "Manager", StringComparison.OrdinalIgnoreCase))
                    return RedirectToAction("Index", "Manager");

                var form = await _formService.GetActiveFormAsync();
                if (form != null)
                {
                    return RedirectToAction("ShowForm", "Questionnaire", new { formId = 1 });
                }

                TempData["ErrorMessage"] = "No active questionnaires available.";
                return RedirectToAction("Login", "Account");
            }

            // If not a regular user, try ReportUser login
            var reportUser = await _context.ReportUsers
                .Include(ru => ru.ReportRole)
                .FirstOrDefaultAsync(ru => ru.LevCompanyId == model.CompanyId && 
                                          ru.Password == model.Password && 
                                          ru.IsActive);

            if (reportUser != null)
            {
                // ReportUser login - Store in session
                HttpContext.Session.SetInt32("ReportUserLevCompanyId", reportUser.LevCompanyId);
                HttpContext.Session.SetString("UserRole", "ReportUser");
                HttpContext.Session.SetString("UserName", $"Report User - {reportUser.LevCompanyId}");
                HttpContext.Session.SetString("ReportRoleName", reportUser.ReportRole.Name);
                HttpContext.Session.SetString("LastActivity", DateTime.UtcNow.ToString());

                // Redirect to reports index
                return RedirectToAction("Index", "Reports");
            }

            // Neither user type found
            TempData["LoginError"] = "Invalid credentials. Please check your Company ID and password.";
            return RedirectToAction("Login", new { error = "1" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
