using GlasAnketa.Services.Interfaces;
using GlasAnketa.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlasAnketa.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionFormService _questionFormService;

        public AnswerController(IAnswerService answerService, IQuestionFormService questionFormService)
        {
            _answerService = answerService;
            _questionFormService = questionFormService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAnswers(FormSubmissionVM model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Save the submitted answers
            var answers = model.Answers.ToDictionary(
                a => a.QuestionId,
                a => (object)(a.ScaleValue ?? (object)a.TextValue)
            );

            await _answerService.SubmitAnswersAsync(userId.Value, model.QuestionFormId, answers);

            // Get next form
            var nextForm = await _questionFormService.GetNextActiveFormAsync(model.QuestionFormId);

            if (nextForm == null)
                return RedirectToAction("ThankYou", "Questionnaire");

            // Redirect instead of returning View (to avoid losing state)
            return RedirectToAction("ShowForm", "Questionnaire", new { formId = nextForm.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Next(FormSubmissionVM model)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                    return RedirectToAction("Login", "Account");

                // Submit answers from active form
                var answers = model.Answers
                    .Where(a => a.ScaleValue.HasValue || !string.IsNullOrWhiteSpace(a.TextValue))
                    .ToDictionary(
                        a => a.QuestionId,
                        a => (object)(a.ScaleValue ?? (object)a.TextValue)
                    );

                if (answers.Any())
                {
                    await _answerService.SubmitAnswersAsync(userId.Value, model.QuestionFormId, answers);
                }
                
                // Go to next form
                var nextForm = await _questionFormService.GetNextActiveFormAsync(model.QuestionFormId);
                if (nextForm == null)
                    return RedirectToAction("ThankYou", "Questionnaire");
                return RedirectToAction("ShowForm", "Questionnaire", new { formId = nextForm.Id });
            }
            catch (Exception ex)
            {
                // Log the error and redirect to the same form
                Console.WriteLine($"Error in Next action: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again.";
                return RedirectToAction("ShowForm", "Questionnaire", new { formId = model.QuestionFormId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Skip(FormSubmissionVM model)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                    return RedirectToAction("Login", "Account");

                // Clean answers (if any) from active form
                await _answerService.ClearAnswersAsync(userId.Value, model.QuestionFormId);
                
                // Go to next form
                var nextForm = await _questionFormService.GetNextActiveFormAsync(model.QuestionFormId);
                if (nextForm == null)
                    return RedirectToAction("ThankYou", "Questionnaire");
                return RedirectToAction("ShowForm", "Questionnaire", new { formId = nextForm.Id });
            }
            catch (Exception ex)
            {
                // Log the error and redirect to the same form
                Console.WriteLine($"Error in Skip action: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again.";
                return RedirectToAction("ShowForm", "Questionnaire", new { formId = model.QuestionFormId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Back(FormSubmissionVM model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            await _answerService.ClearAnswersAsync(userId.Value, model.QuestionFormId);
            var prevForm = await _questionFormService.GetPreviousActiveFormAsync(model.QuestionFormId);
            var targetId = prevForm?.Id ?? model.QuestionFormId;
            return RedirectToAction("ShowForm", "Questionnaire", new { formId = targetId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(FormSubmissionVM model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Submit answers from active form
            var answers = model.Answers.ToDictionary(
                a => a.QuestionId,
                a => (object)(a.ScaleValue ?? (object)a.TextValue)
            );
            await _answerService.SubmitAnswersAsync(userId.Value, model.QuestionFormId, answers);
            
            // Clear session and go to log off view with message
            TempData["FinishMessage"] = "Blagodaram na Vaseto ucestvo";
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account", new { message = "thankyou" });
        }
    }

}
