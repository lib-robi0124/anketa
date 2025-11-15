using GlasAnketa.Services.Interfaces;
using GlasAnketa.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

namespace GlasAnketa.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IReportService reportService, ILogger<ReportsController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        private bool IsUserAdministrator()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            // Allow both Administrator and Manager to access reports
            return !string.IsNullOrEmpty(userRole) && (userRole == "Administrator" || userRole == "Manager");
        }

        private IActionResult RedirectToLoginIfNotAdmin()
        {
            if (!IsUserAdministrator())
            {
                _logger.LogWarning("Unauthorized access attempt to reports by user with role: {UserRole}", 
                    HttpContext.Session.GetString("UserRole") ?? "Unknown");
                return RedirectToAction("Login", "Account", new { returnUrl = Request.Path });
            }
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                _logger.LogInformation("Loading report summary");
                var summary = await _reportService.GetReportSummaryAsync();
                _logger.LogInformation($"Report summary loaded successfully: {summary.TotalUsers} users, {summary.TotalForms} forms");
                return View(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading report summary");
                ModelState.AddModelError("", "Error loading report summary. Please try again.");
                return View(new ReportSummaryVM());
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetReportSummary()
        {
            if (!IsUserAdministrator())
            {
                _logger.LogWarning("Unauthorized AJAX access attempt to GetReportSummary by user with role: {UserRole}", 
                    HttpContext.Session.GetString("UserRole") ?? "Unknown");
                return Json(new { error = "Unauthorized access" });
            }

            try
            {
                _logger.LogInformation("Loading report summary via AJAX");
                var summary = await _reportService.GetReportSummaryAsync();
                _logger.LogInformation($"Report summary loaded via AJAX successfully");
                return Json(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading report summary via AJAX");
                return Json(new { error = "Error loading report summary" });
            }
        }
        public async Task<IActionResult> QuestionReports()
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                _logger.LogInformation("Loading question reports");
                var reports = await _reportService.GetQuestionReportsAsync();
                _logger.LogInformation($"Question reports loaded successfully: {reports.Count} reports");
                return View(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading question reports");
                ModelState.AddModelError("", "Error loading question reports. Please try again.");
                return View(new List<QuestionReportVM>());
            }
        }

        public async Task<IActionResult> FormReports()
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                _logger.LogInformation("Loading form reports");
                var reports = await _reportService.GetFormReportsAsync();
                _logger.LogInformation($"Form reports loaded successfully: {reports.Count} reports");
                return View(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading form reports");
                ModelState.AddModelError("", "Error loading form reports. Please try again.");
                return View(new List<FormReportVM>());
            }
        }

        public async Task<IActionResult> QuestionByOUReports()
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                _logger.LogInformation("Loading question by OU reports");
                var reports = await _reportService.GetQuestionByOUReportsAsync();
                _logger.LogInformation($"Question by OU reports loaded successfully: {reports.Count} reports");
                return View(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading question by OU reports");
                ModelState.AddModelError("", "Error loading question by OU reports. Please try again.");
                return View(new List<QuestionByOUMReportVM>());
            }
        }

        public async Task<IActionResult> FormByOUReports()
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                _logger.LogInformation("Loading form by OU reports");
                var reports = await _reportService.GetFormByOUReportsAsync();
                _logger.LogInformation($"Form by OU reports loaded successfully: {reports.Count} reports");
                return View(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading form by OU reports");
                ModelState.AddModelError("", "Error loading form by OU reports. Please try again.");
                return View(new List<FormByOUMReportVM>());
            }
        }

        public async Task<IActionResult> AgeReports(string? ageRange)
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                // Admin and Manager both see aggregated data across all companies
                int? companyId = null;

                _logger.LogInformation("Loading age reports for all companies with ageRange {AgeRange}", ageRange);
                var reports = await _reportService.GetAgeReportsAsync(companyId);
                if (!string.IsNullOrWhiteSpace(ageRange))
                {
                    reports = reports.Where(r => string.Equals(r.AgeRange, ageRange, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }
                _logger.LogInformation("Age reports loaded successfully: {Count} buckets", reports.Count);
                return View(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading age reports");
                ModelState.AddModelError("", "Error loading age reports. Please try again.");
                return View(new List<AgeReportVM>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExportAgeReports(string? ageRange)
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                // Admin and Manager both export aggregated data across all companies
                int? companyId = null;

                _logger.LogInformation("Exporting age reports to CSV for all companies with ageRange {AgeRange}", ageRange);
                var reports = await _reportService.GetAgeReportsAsync(companyId);
                if (!string.IsNullOrWhiteSpace(ageRange))
                {
                    reports = reports.Where(r => string.Equals(r.AgeRange, ageRange, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }
                _logger.LogInformation("Age reports exported successfully: {Count} buckets", reports.Count);
                var filename = companyId.HasValue ? $"age_reports_{companyId.Value}.csv" : "age_reports.csv";
                return ExportToCsv(reports, filename);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting age reports to CSV");
                return RedirectToAction("AgeReports", new { ageRange, error = "Failed to export reports" });
            }
        }

        public async Task<IActionResult> WorkExperienceReports(string? experienceRange)
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                // Admin and Manager both see aggregated data across all companies
                int? companyId = null;

                _logger.LogInformation("Loading work experience reports for all companies with range {ExperienceRange}", experienceRange);
                var reports = await _reportService.GetWorkExperienceReportsAsync(companyId);
                if (!string.IsNullOrWhiteSpace(experienceRange))
                {
                    reports = reports.Where(r => string.Equals(r.ExperienceRange, experienceRange, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }
                _logger.LogInformation("Work experience reports loaded successfully: {Count} buckets", reports.Count);
                return View(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading work experience reports");
                ModelState.AddModelError("", "Error loading work experience reports. Please try again.");
                return View(new List<WorkExperienceReportVM>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExportWorkExperienceReports(string? experienceRange)
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                // Admin and Manager both export aggregated data across all companies
                int? companyId = null;

                _logger.LogInformation("Exporting work experience reports to CSV for all companies with range {ExperienceRange}", experienceRange);
                var reports = await _reportService.GetWorkExperienceReportsAsync(companyId);
                if (!string.IsNullOrWhiteSpace(experienceRange))
                {
                    reports = reports.Where(r => string.Equals(r.ExperienceRange, experienceRange, System.StringComparison.OrdinalIgnoreCase)).ToList();
                }
                _logger.LogInformation("Work experience reports exported successfully: {Count} buckets", reports.Count);
                var filename = companyId.HasValue ? $"work_experience_reports_{companyId.Value}.csv" : "work_experience_reports.csv";
                return ExportToCsv(reports, filename);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting work experience reports to CSV");
                return RedirectToAction("WorkExperienceReports", new { experienceRange, error = "Failed to export reports" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExportQuestionReports()
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                _logger.LogInformation("Exporting question reports to CSV");
                var reports = await _reportService.GetQuestionReportsAsync();
                _logger.LogInformation($"Question reports exported successfully: {reports.Count} reports");
                return ExportToCsv(reports, "question_reports.csv");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting question reports to CSV");
                return RedirectToAction("QuestionReports", new { error = "Failed to export reports" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExportFormReports()
        {
            var authResult = RedirectToLoginIfNotAdmin();
            if (authResult != null) return authResult;

            try
            {
                _logger.LogInformation("Exporting form reports to CSV");
                var reports = await _reportService.GetFormReportsAsync();
                _logger.LogInformation($"Form reports exported successfully: {reports.Count} reports");
                return ExportToCsv(reports, "form_reports.csv");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting form reports to CSV");
                return RedirectToAction("FormReports", new { error = "Failed to export reports" });
            }
        }

        private IActionResult ExportToCsv<T>(List<T> data, string filename)
        {
            try
            {
                if (data == null || !data.Any())
                {
                    _logger.LogWarning($"ExportToCsv: No data provided for {filename}");
                    return RedirectToAction("Index", new { error = "No data to export" });
                }

                var sb = new StringBuilder();

                // Get properties for header
                var properties = typeof(T).GetProperties();
                sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

                // Add data rows
                foreach (var item in data)
                {
                    var values = properties.Select(p =>
                    {
                        var value = p.GetValue(item, null);
                        return value?.ToString()?.Replace(",", ";") ?? "";
                    });
                    sb.AppendLine(string.Join(",", values));
                }

                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                _logger.LogInformation($"ExportToCsv: Exported {data.Count} records to {filename}");
                return File(bytes, "text/csv", filename);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error exporting data to CSV file {filename}");
                return RedirectToAction("Index", new { error = "Export failed" });
            }
        }
    }
}
