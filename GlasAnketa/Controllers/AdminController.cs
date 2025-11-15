using GlasAnketa.Services.Implementations;
using GlasAnketa.Services.Interfaces;
using GlasAnketa.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GlasAnketa.Controllers
{
    public class AdminController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IQuestionFormService _formService;
        private readonly IAnswerService _answerService;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;

        public AdminController(IQuestionService questionService,
                             IQuestionFormService formService,
                             IAnswerService answerService,
                             IUserService userService,
                             IReportService reportService)
        {
            _questionService = questionService;
            _formService = formService;
            _answerService = answerService;
            _userService = userService;
            _reportService = reportService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewResults()
        {
            // Redirect to the main reports dashboard
            return RedirectToAction("Index", "Reports");
        }
       
        [HttpGet]
        public async Task<IActionResult> ManageQuestions()
        {
            var forms = await _formService.GetAllFormsAsync();
            ViewBag.QuestionForms = new SelectList(forms, "Id", "Title");

            // Get all questions from all forms
            var allQuestions = new List<QuestionVM>();
            foreach (var form in forms)
            {
                allQuestions.AddRange(form.Questions);
            }

            return View(allQuestions);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            // Basic role check: only Administrator can manage users
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You are not authorized to manage users.";
                return RedirectToAction("Index");
            }

            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var isAdmin = string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase);
            var isManager = string.Equals(role, "Manager", StringComparison.OrdinalIgnoreCase);

            if (!isAdmin && !isManager)
            {
                TempData["ErrorMessage"] = "You are not authorized to create users.";
                return RedirectToAction("Index");
            }

            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Administrator" },
                new SelectListItem { Value = "2", Text = "Employee", Selected = true },
                new SelectListItem { Value = "3", Text = "Manager" }
            };

            return View(new RegisterUserVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterUserVM model, int selectedRoleId)
        {
            var role = HttpContext.Session.GetString("UserRole");
            var isAdmin = string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase);
            var isManager = string.Equals(role, "Manager", StringComparison.OrdinalIgnoreCase);

            if (!isAdmin && !isManager)
            {
                TempData["ErrorMessage"] = "You are not authorized to create users.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Administrator", Selected = selectedRoleId == 1 },
                    new SelectListItem { Value = "2", Text = "Employee", Selected = selectedRoleId == 2 },
                    new SelectListItem { Value = "3", Text = "Manager", Selected = selectedRoleId == 3 }
                };
                return View(model);
            }

            try
            {
                // Managers are forced to create Employee users; Admins may pick any role
                var effectiveRoleId = isAdmin ? selectedRoleId : 2;

                var roleVm = new RoleVM
                {
                    RoleId = effectiveRoleId,
                    Name = effectiveRoleId == 1 ? "Administrator"
                         : effectiveRoleId == 3 ? "Manager"
                         : "Employee"
                };

                await _userService.RegisterUser(model, roleVm);
                TempData["SuccessMessage"] = "User created successfully.";

                // Admins go to ManageUsers list; Managers go back to Manager dashboard
                if (isAdmin)
                {
                    return RedirectToAction("ManageUsers");
                }
                else
                {
                    return RedirectToAction("Index", "Manager");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating user: {ex.Message}");
                ViewBag.Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Administrator", Selected = selectedRoleId == 1 },
                    new SelectListItem { Value = "2", Text = "Employee", Selected = selectedRoleId == 2 },
                    new SelectListItem { Value = "3", Text = "Manager", Selected = selectedRoleId == 3 }
                };
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You are not authorized to manage users.";
                return RedirectToAction("Index");
            }

            var user = await _userService.GetUserForEditAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("ManageUsers");
            }

            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Administrator", Selected = user.Role?.RoleId == 1 },
                new SelectListItem { Value = "2", Text = "Employee", Selected = user.Role?.RoleId == 2 },
                new SelectListItem { Value = "3", Text = "Manager", Selected = user.Role?.RoleId == 3 }
            };

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserVM model, int selectedRoleId)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You are not authorized to manage users.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Administrator", Selected = selectedRoleId == 1 },
                    new SelectListItem { Value = "2", Text = "Employee", Selected = selectedRoleId == 2 },
                    new SelectListItem { Value = "3", Text = "Manager", Selected = selectedRoleId == 3 }
                };
                return View(model);
            }

            model.Role = new RoleVM
            {
                RoleId = selectedRoleId,
                Name = selectedRoleId == 1 ? "Administrator" : selectedRoleId == 3 ? "Manager" : "Employee"
            };

            var success = await _userService.UpdateUserAsync(model);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to update user.";
            }
            else
            {
                TempData["SuccessMessage"] = "User updated successfully.";
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUserActive(int id, bool isActive)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You are not authorized to manage users.";
                return RedirectToAction("Index");
            }

            var success = await _userService.ToggleUserActiveAsync(id, isActive);
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success
                ? $"User {(isActive ? "activated" : "deactivated")} successfully."
                : "Failed to update user status.";

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public async Task<IActionResult> ResetUserPassword(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You are not authorized to manage users.";
                return RedirectToAction("Index");
            }

            var user = await _userService.GetUserForEditAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("ManageUsers");
            }

            ViewBag.UserId = id;
            ViewBag.UserName = user.FullName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetUserPassword(int id, string newPassword)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "You are not authorized to manage users.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                ModelState.AddModelError(string.Empty, "New password is required.");
                ViewBag.UserId = id;
                return View();
            }

            var success = await _userService.ResetPasswordAsync(id, newPassword);
            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success
                ? "Password reset successfully."
                : "Failed to reset password.";

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public async Task<IActionResult> ManageForms()
        {
            var forms = await _formService.GetAllFormsAsync();
            return View(forms);
        }

        [HttpGet]
        public async Task<IActionResult> CreateQuestion()
        {
            var forms = await _formService.GetAllFormsAsync();
            ViewBag.QuestionForms = new SelectList(forms, "Id", "Title");

            // Mock question types - in real app, get from database
            var questionTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Scale (1-10)" },
                new SelectListItem { Value = "2", Text = "Text" }
            };
            ViewBag.QuestionTypes = new SelectList(questionTypes, "Value", "Text");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(RegisterQuestionVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _questionService.CreateQuestion(model);
                    TempData["SuccessMessage"] = "Question created successfully!";
                    return RedirectToAction("ManageQuestions");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating question: {ex.Message}");
                }
            }

            // Repopulate dropdowns if validation fails
            var forms = await _formService.GetAllFormsAsync();
            ViewBag.QuestionForms = new SelectList(forms, "Id", "Title");

            var questionTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Scale (1-10)" },
                new SelectListItem { Value = "2", Text = "Text" }
            };
            ViewBag.QuestionTypes = new SelectList(questionTypes, "Value", "Text");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditQuestion(int id)
        {
            try
            {
                var question = await _questionService.GetQuestionById(id);
                if (question == null)
                {
                    TempData["ErrorMessage"] = "Question not found.";
                    return RedirectToAction("ManageQuestions");
                }

                var forms = await _formService.GetAllFormsAsync();
                ViewBag.QuestionForms = new SelectList(forms, "Id", "Title");

                var questionTypes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Scale (1-10)", Selected = question.QuestionType == "Scale" },
                    new SelectListItem { Value = "2", Text = "Text", Selected = question.QuestionType == "Text" }
                };
                ViewBag.QuestionTypes = new SelectList(questionTypes, "Value", "Text");

                var editModel = new RegisterQuestionVM
                {
                    Id = question.Id,
                    QuestionFormId = question.QuestionFormId,
                    Text = question.Text,
                    QuestionTypeId = question.QuestionType == "Scale" ? 1 : 2
                };

                return View(editModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading question: {ex.Message}";
                return RedirectToAction("ManageQuestions");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(RegisterQuestionVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _questionService.UpdateQuestion(model);
                    TempData["SuccessMessage"] = "Question updated successfully!";
                    return RedirectToAction("ManageQuestions");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating question: {ex.Message}");
                }
            }

            // Repopulate dropdowns
            var forms = await _formService.GetAllFormsAsync();
            ViewBag.QuestionForms = new SelectList(forms, "Id", "Title");

            var questionTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Scale (1-10)" },
                new SelectListItem { Value = "2", Text = "Text" }
            };
            ViewBag.QuestionTypes = new SelectList(questionTypes, "Value", "Text");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                _questionService.RemoveQuestion(id);
                TempData["SuccessMessage"] = "Question deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting question: {ex.Message}";
            }

            return RedirectToAction("ManageQuestions");
        }

        [HttpGet]
        public async Task<IActionResult> CreateForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForm(QuestionFormVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _formService.CreateFormAsync(model);
                    TempData["SuccessMessage"] = "Form created successfully!";
                    return RedirectToAction("ManageForms");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating form: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditForm(int id)
        {
            try
            {
                var form = await _formService.GetFormByIdAsync(id);
                if (form == null)
                {
                    TempData["ErrorMessage"] = "Form not found.";
                    return RedirectToAction("ManageForms");
                }

                return View(form);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading form: {ex.Message}";
                return RedirectToAction("ManageForms");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditForm(QuestionFormVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var success = await _formService.UpdateFormAsync(model);
                    if (success)
                    {
                        TempData["SuccessMessage"] = "Form updated successfully!";
                        return RedirectToAction("ManageForms");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to update form.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating form: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(int id)
        {
            try
            {
                var success = await _formService.DeleteFormAsync(id);
                if (success)
                {
                    TempData["SuccessMessage"] = "Form deleted successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete form.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting form: {ex.Message}";
            }

            return RedirectToAction("ManageForms");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleFormStatus(int id, bool isActive)
        {
            try
            {
                var success = await _formService.ToggleFormStatusAsync(id, isActive);
                if (success)
                {
                    TempData["SuccessMessage"] = $"Form {(isActive ? "activated" : "deactivated")} successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to update form status.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error updating form status: {ex.Message}";
            }

            return RedirectToAction("ManageForms");
        }

        [HttpGet]
        public async Task<IActionResult> ViewResults(int formId = 1)
        {
            try
            {
                var answers = await _answerService.GetFormAnswersAsync(formId);
                var summaries = await _answerService.GetAnswerSummariesAsync(formId);

                var viewModel = new ResultsVM
                {
                    Answers = answers,
                    Summaries = summaries
                };

                ViewBag.SelectedFormId = formId;
                var forms = await _formService.GetAllFormsAsync();
                ViewBag.Forms = new SelectList(forms, "Id", "Title", formId);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ViewResults: {ex.Message}");
                return View(new ResultsVM { Answers = new List<AnswerVM>(), Summaries = new Dictionary<int, AnswerSummaryVM>() });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportResults(int formId)
        {
            var answers = await _answerService.GetFormAnswersAsync(formId);

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("UserId,CompanyId,QuestionFormId,QuestionId,ScaleValue,TextValue,AnsweredDate");

            foreach (var a in answers)
            {
                var line = string.Join(",",
                    a.UserId,
                    a.CompanyId,
                    a.QuestionFormId,
                    a.QuestionId,
                    a.ScaleValue?.ToString() ?? string.Empty,
                    (a.TextValue ?? string.Empty).Replace("\"", "''"),
                    a.AnsweredDate.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendLine(line);
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            var fileName = $"Form_{formId}_Results_{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
            return File(bytes, "text/csv", fileName);
        }
    }
}
