# QuestionReportByFilters — Implementation Summary (Updated)

This document captures all code changes to add the filtered report “QuestionReportByFilters”, link it from Admin, resolve view errors, and update the questionnaire form navigation and submission flow.

## New Files

- `GlasAnketa.ViewModels/Models/QuestionReportByFiltersVM.cs`
- `GlasAnketa/Views/Reports/QuestionReportByFilters.cshtml`

## Modified Files (Reports)

- `GlasAnketa.Services/Interfaces/IReportService.cs`
  - Added: `Task<List<QuestionReportByFiltersVM>> GetQuestionReportByFiltersAsync(int? companyId = null, string? ou = null, string? ou2 = null);`
- `GlasAnketa.Services/Implementations/ReportService.cs`
  - Implemented filtered per‑question aggregation with demographics
  - Average computed as `TotalScale / TotalResponses`
- `GlasAnketa/Controllers/ReportsController.cs`
  - Added `QuestionReportByFilters(int? companyId, string? ou, string? ou2)` action to render the new view
- `GlasAnketa/Views/Admin/Index.cshtml`
  - Added single card link to `@Url.Action("QuestionReportByFilters", "Reports")`
  - Removed duplicate “Question Reports by Filters” card

## Modified Files (Questionnaire Navigation)

- `GlasAnketa/Views/Questionnaire/Form.cshtml`
  - Removed form counter “Questionnaire n of m”
  - Removed `*` from question text display
  - Replaced the single submit button with four buttons:
    - Back: posts to `Answer/Back`
    - Skip: posts to `Answer/Skip`
    - Next: posts to `Answer/Next`
    - Finish: posts to `Answer/Finish`
  - Scale options styled as colored circles (1–10) that fill on click
- `GlasAnketa/Controllers/AnswerController.cs`
  - Added actions to support new buttons:
    - `Next(FormSubmissionVM model)` saves answers then navigates to next active form
    - `Skip(FormSubmissionVM model)` clears answers then navigates to next active form
    - `Back(FormSubmissionVM model)` clears answers then navigates to previous active form
    - `Finish(FormSubmissionVM model)` saves answers then redirects to Login with finish message
- `GlasAnketa/Views/Questionnaire/ThankYou.cshtml`
  - Displays message when used; current Finish flow goes to Login
  - Uses a POST Logout button if shown
- `GlasAnketa/Views/Account/Login.cshtml`
  - Shows green alert “Blagodaram za Vaseto ucestvo vo Anketata” when `message=thankyou` or `TempData["FinishMessage"]` is set

## Modified Files (Services & DataAccess for Navigation)

- `GlasAnketa.Services/Interfaces/IAnswerService.cs`
  - Added: `Task<bool> ClearAnswersAsync(int userId, int formId);`
- `GlasAnketa.Services/Implementations/AnswerService.cs`
  - Implemented `ClearAnswersAsync` via repository
- `GlasAnketa.DataAccess/Interfaces/IAnswerRepository.cs`
  - Added: `Task<bool> ClearAnswersAsync(int userId, int formId);`
- `GlasAnketa.DataAccess/Implementations/AnswerRepository.cs`
  - Implemented answer deletion for a user+form
- `GlasAnketa.DataAccess/Interfaces/IQuestionFormRepository.cs`
  - Added: `Task<QuestionForm?> GetPreviousActiveFormAsync(int currentFormId);`
- `GlasAnketa.DataAccess/Implementations/QuestionFormRepository.cs`
  - Implemented previous‑form lookup with wrap‑around in the 1..13 active forms range
- `GlasAnketa.Services/Interfaces/IQuestionFormService.cs`
  - Added: `Task<QuestionFormVM?> GetPreviousActiveFormAsync(int currentFormId);`
- `GlasAnketa.Services/Implementations/QuestionFormService.cs`
  - Implemented mapping to return previous active form

## Key Behaviors

- Reports
  - Filter by CompanyId/OU/OU2; demographic columns rendered dynamically
  - Average = TotalScale / TotalResponses; sample text responses show when present
- Questionnaire Navigation
  - Back clears answers and goes to previous active form
  - Skip clears answers and goes to next active form
  - Next saves answers and goes to next active form
  - Finish saves answers and redirects to Login showing the thank-you message

## Paths To Verify

- View rendering (Reports):
  - `/Reports/QuestionReportByFilters` → should load and show filters bar and results
- Form flow (Questionnaire):
  - `/Questionnaire/ShowForm?formId=1` → header without counter; questions without `*`; buttons Back/Skip/Next/Finish work as described

## Build Status

- Verified solution build succeeds; warnings only (no errors)

## UI Styling Changes (Scale)

- Scale choices display as circular buttons with numbers 1–10.
- Default: white background, `#3498db` border and text.
- Hover: soft outer glow.
- Selected: filled `#3498db` circle with white text.