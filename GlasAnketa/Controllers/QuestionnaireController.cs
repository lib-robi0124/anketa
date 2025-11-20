using GlasAnketa.Services.Interfaces;
using GlasAnketa.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlasAnketa.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly IQuestionFormService _questionFormService;

        public QuestionnaireController(IQuestionFormService questionFormService)
        {
            _questionFormService = questionFormService;
        }
        // GET: Questionnaire/Form/{id}
        [HttpGet]
        public async Task<IActionResult> ShowForm(int? formId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var form = formId.HasValue
                ? await _questionFormService.GetFormByIdAsync(formId.Value)
                : await _questionFormService.GetActiveFormAsync();

            if (form == null)
                return RedirectToAction("ThankYou");

            var vm = new FormSubmissionVM
            {
                QuestionForm = form,
                QuestionFormId = form.Id,
                Answers = form.Questions.Select(q => new AnswerVM
                {
                    QuestionId = q.Id,
                    QuestionFormId = form.Id,
                    UserId = userId.Value
                }).ToList()
            };

            // Check if there's a previous form to determine if Back button should be shown
            var previousForm = await _questionFormService.GetPreviousActiveFormAsync(form.Id);
            ViewBag.IsFirstForm = previousForm == null;

            // Check if there's a next form to determine if Next/Skip buttons should be shown
            var nextForm = await _questionFormService.GetNextActiveFormAsync(form.Id);
            ViewBag.IsLastForm = nextForm == null;

            return View("Form", vm);
        }
        
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}