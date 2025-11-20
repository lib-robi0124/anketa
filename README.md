# anketa
employee satisfaction survey
file: d:\proekt-office\officeApps\anketa\README.md
# GlasAnketa Code Map

This document summarizes the key C# and Razor (`.cshtml`) parts of the system — from Domain models, DataSeed, Data Access (repositories), Services, Views — ending at `Program.cs`. Each item includes brief intent-focused comments such as “submits answers to DB”, “loads data for reports”, “advanced dashboard for Manager”.

## Domain Models (GlasAnketa.Domain/Models)
- `Answer.cs`
  - Tracks per-question responses, including `ScaleValue` and `TextValue`, plus demographics (`Age`, `WorkExperience`).
- `Question.cs`
  - Defines a question linked to `QuestionType` and a `QuestionForm`; marked `IsRequired`.
- `QuestionForm.cs`
  - A survey form composed of multiple `Question`s, collecting `Answer`s; `IsActive` drives availability.
- `User.cs`
  - Employee or admin with `CompanyId`, OU/OU2, role, demographics; links to all their `Answer`s.
- `QuestionType.cs`
  - Type metadata (“Scale”, “Text”) and description; links to `Question`s.
- `Role.cs`
  - Role entity (“Administrator”, “Employee”, “Manager”), with navigation to assigned `User`s.
- `AnswerSummary.cs`
  - Aggregate container for reporting per-question totals, averages, distributions, and text samples.

## Data Access
- `DataContext/AppDbContext.cs`
  - Configures EF Core sets and relationships; seeds data and indices; enforces NoAction deletes.
  - Seeds via `modelBuilder.SeedData()` at `GlasAnketa.DataAccess/DataContext/AppDbContext.cs:19`.
- `Extensions/DataSeedExtensions.cs`
  - Seeds `Role`s, `QuestionType`s, and an extensive `User` list for demos and reports.
- Generic Repository (CRUD)
  - `Implementations/Repository.cs`
    - `GetActiveAsync`, `GetAllAsync`, `AddAsync`, `Update`, `Remove` for any entity.
- Answer Repository
  - `Implementations/AnswerRepository.cs`
    - Submit answers to DB: `SubmitAnswersAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\AnswerRepository.cs:100
    - Save/Upsert answers: `SaveAnswersAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\AnswerRepository.cs:78
    - Build per-question summaries: `GetAnswerSummariesAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\AnswerRepository.cs:29
    - Fetch helpers: by form, question, user, distinct user ids.
- Question Form Repository
  - `Implementations/QuestionFormRepository.cs`
    - Load full forms with questions: `GetAllFormQuestionsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\QuestionFormRepository.cs:12
    - Next active form in rotating sequence: `GetNextActiveFormAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\QuestionFormRepository.cs:20
    - Toggle activation: `ToggleFormStatusAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\QuestionFormRepository.cs:60
- Question Repository
  - `Implementations/QuestionRepository.cs`
    - Load a form with its questions and types: `GetFormWithQuestionsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\QuestionRepository.cs:14
- User Repository
  - `Implementations/UserRepository.cs`
    - Authenticate user: `AuthenticateAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.DataAccess\Implementations\UserRepository.cs:12
    - Load users (with roles), by ID or company ID, and OU fetch.

## Services
- DI Helpers
  - `Extensions/InjectionExtensions.cs`
    - Central registration of DbContext, repositories, services, and AutoMapper.
- AutoMapper Profiles
  - `AutoMappers/*.cs`
    - Map Domain ↔ ViewModels for `User`, `Role`, `Question`, `QuestionForm`, `QuestionType`, `Answer`/`AnswerSummary`.
- Answer Service
  - `Implementations/AnswerService.cs`
    - Wraps repository calls; main submit: `SubmitAnswersAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\AnswerService.cs:49
- Report Service
  - `Implementations/ReportService.cs`
    - Loads data for reports:
      - Question report list: `GetQuestionReportsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\ReportService.cs:36
      - Form report list: `GetFormReportsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\ReportService.cs:107
      - Question-by-OU: `GetQuestionByOUReportsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\ReportService.cs:157
      - Form-by-OU: `GetFormByOUReportsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\ReportService.cs:217
      - Summary dashboard: `GetReportSummaryAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\ReportService.cs:277
      - Age buckets: `GetAgeReportsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\ReportService.cs:321
      - Work experience buckets: `GetWorkExperienceReportsAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\ReportService.cs:407
- Question Service
  - `Implementations/QuestionService.cs`
    - Admin operations for questions: create, list, get-by-id, update, delete.
- Question Form Service
  - `Implementations/QuestionFormService.cs`
    - Admin operations for forms: create, update, delete, list; next-active discovery; toggle active.
    - Next form for flow: `GetNextActiveFormAsync` d:\proekt-office\officeApps\anketa\GlasAnketa.Services\Implementations\QuestionFormService.cs:80
- User Service
  - `Implementations/UserService.cs`
    - Register user, validate login, CRUD for users, reset password; admin creates questions/forms.

## Controllers (key flow points)
- Answer submission
  - `GlasAnketa/Controllers/AnswerController.cs`
    - Submits a questionnaire to DB: `SubmitAnswers` d:\proekt-office\officeApps\anketa\GlasAnketa\Controllers\AnswerController.cs:20
    - Builds `Dictionary<int, object>` and delegates to `AnswerService.SubmitAnswersAsync`.
- Reports
  - `GlasAnketa/Controllers/ReportsController.cs`
    - Loads report pages and AJAX summary; CSV export endpoints for all report types.
- Admin
  - `GlasAnketa/Controllers/AdminController.cs`
    - Admin dashboards and management for users, questions, forms; export user-answer status CSV.
- Questionnaire
  - `GlasAnketa/Controllers/QuestionnaireController.cs`
    - Loads current/selected form for user; renders form; shows “Thank You”.
- Account
  - `GlasAnketa/Controllers/AccountController.cs`
    - Login and logout; routes user to Admin, Manager, or Questionnaire based on role.

## Views (Razor .cshtml)
- Account
  - `Views/Account/Login.cshtml` — Login form with validation and error modal.
- Questionnaire
  - `Views/Questionnaire/Form.cshtml` — Survey form renderer; posts to `SubmitAnswers`.
  - `Views/Questionnaire/ThankYou.cshtml` — Completion page.
- Manager
  - `Views/Manager/Index.cshtml` — Advanced dashboard for Manager: KPIs, report shortcuts, summary cards.
- Admin
  - `Views/Admin/Index.cshtml` — Admin dashboard with user answer status overview.
  - `Views/Admin/ManageUsers.cshtml`, `EditUser.cshtml`, `ResetUserPassword.cshtml` — Admin user management.
  - `Views/Admin/ManageForms.cshtml`, `CreateForm.cshtml`, `EditForm.cshtml` — Form CRUD views.
  - `Views/Admin/ManageQuestions.cshtml`, `CreateQuestion.cshtml`, `EditQuestion.cshtml` — Question CRUD views.
  - `Views/Admin/ViewResults.cshtml` — Results viewer and export link (redirects to Reports).
- Reports
  - `Views/Reports/Index.cshtml` — Reports dashboard; overall summary metrics and links.
  - `Views/Reports/QuestionReports.cshtml` — Per-question analysis with export CSV.
  - `Views/Reports/FormReports.cshtml` — Per-form analysis.
  - `Views/Reports/QuestionByOUReports.cshtml` — Question metrics by OU/OU2.
  - `Views/Reports/FormByOUReports.cshtml` — Form metrics by OU/OU2.
  - `Views/Reports/AgeReports.cshtml` — Age-bucket metrics and CSV export.
  - `Views/Reports/WorkExperienceReports.cshtml` — Work-experience bucket metrics and CSV export.
- Shared and Misc
  - `Views/Shared/_Layout.cshtml`, `Views/_ViewStart.cshtml`, `Views/_ViewImports.cshtml`, `Views/Shared/Error.cshtml`, etc. — Shell, imports, and error handling.
  - `Views/Home/Index.cshtml`, `Views/Home/Privacy.cshtml` — Home placeholders.

## Program Setup
- `GlasAnketa/Program.cs`
  - Adds MVC, session (`IdleTimeout=30m`, cookie `QuestionnaireSession`), cookie auth, authorization.
  - Registers DbContext (SQL Server, `DefaultConnection`) and DI for repositories and services.
  - Registers AutoMapper profiles.
  - Middleware order: `UseHttpsRedirection`, `UseStaticFiles`, `UseRouting`, `UseAuthentication`, `UseAuthorization`, `UseSession`.
  - Default route: `Account/Login` to start the flow.

## Typical Flow
- User logs in (`AccountController.Login`), session is set, routed by role.
- Employee sees active form (`QuestionnaireController.ShowForm`) and submits answers (`AnswerController.SubmitAnswers`) which upserts via `AnswerRepository.SubmitAnswersAsync`.
- Manager/Admin use dashboards and reports; data loaded via `ReportService` and displayed in `Views/Reports/*.cshtml`.
- Admin manages users, questions, forms in `Views/Admin/*` backed by respective services.

