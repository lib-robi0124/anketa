# Project Changes Summary

## 1. Login Error Handling

**Purpose:** Show a friendly popup on invalid credentials instead of an exception.

**Key changes:**
- **Controllers/AccountController.cs**
  - Uses `IUserService.ValidateUser` and checks for `null` to detect invalid login.
  - On invalid login, sets `TempData["LoginError"] = "wrong user or pass"` and redirects back to `Login` with `?error=1`.
  - Logout action clears session and redirects to `Login`.
- **Services/Interfaces/IUserService.cs**
  - `ValidateUser` signature changed to `Task<UserVM?> ValidateUser(UserCredentialsVM userCredentialsVM)`.
- **Services/Implementations/UserService.cs**
  - `ValidateUser` calls `AuthenticateAsync` and returns `null` instead of throwing `InvalidOperationException` on invalid credentials.

## 2. Age-Based Reports

**Purpose:** Correct age reports using Answer data (Age, CompanyId, ScaleValue) and provide richer metrics.

**Key changes:**
- **ViewModels/Models/AgeReportVM.cs**
  - Added:
    - `int CountUsers` (distinct CompanyIds in age bucket).
    - `int QuestionsAnswered` (number of scale answers in bucket).
    - `int TotalScaleValue` (sum of scale values).
    - `string? CompanyIdList` (comma-separated list of distinct CompanyIds).
    - `double AverageScaleValue` / `AverageScaleValueDisplay` (sum / questions).
    - `int TotalResponses` kept for backward compatibility (matches `CountUsers`).
- **Services/Implementations/ReportService.cs**
  - `GetAgeReportsAsync(int? companyId = null)` now:
    - Optionally filters by `companyId` (Manager only; Admin sees all).
    - Buckets by age ranges: `<25`, `25-30`, `31-40`, `41-50`, `>50`.
    - Computes `CountUsers`, `QuestionsAnswered`, `TotalScaleValue`, `AverageScaleValue`.
    - Builds `CompanyIdList` from distinct `Answer.CompanyId` values.
- **Controllers/ReportsController.cs**
  - `AgeReports(string? ageRange)` and `ExportAgeReports(string? ageRange)`:
    - Both Admin and Manager now use `GetAgeReportsAsync(null)`, so they see the **same aggregated values** (Questions Answered, Sum of Scale Values, Average) across all companies.
    - Role only affects **which columns are visible** in the view (Admin sees Users + CompanyIds; Manager does not).
- **Views/Reports/AgeReports.cshtml**
  - New table columns:
    - `Age Range`, `Users`, `Questions Answered`, `Sum of Scale Values`, `Average (Sum / Questions)`.
    - **Admin-only** column `CompanyIds` (shows `CompanyIdList`).

## 3. Work Experience Reports

**Purpose:** Add WorkExperience-based report with ranges and correct aggregation.

**Key changes:**
- **ViewModels/Models/WorkExperienceReportVM.cs** (new + extended)
  - Properties:
    - `string ExperienceRange`, `int? CompanyId`.
    - `int CountUsers`, `int QuestionsAnswered`, `int TotalScaleValue`.
    - `string? CompanyIdList` (distinct CompanyIds in range).
    - `int TotalResponses` (matches `CountUsers`).
    - `double AverageScaleValue` / `AverageScaleValueDisplay` (sum / questions).
- **Services/Interfaces/IReportService.cs**
  - Added `Task<List<WorkExperienceReportVM>> GetWorkExperienceReportsAsync(int? companyId = null);`.
- **Services/Implementations/ReportService.cs**
  - `GetWorkExperienceReportsAsync(int? companyId = null)`:
    - Optional filter by `companyId` (Admin sees all, Manager sees own company).
    - Buckets by `WorkExperience` ranges: `<5`, `5-10`, `10-15`, `15-20`, `>20`.
    - Computes `CountUsers`, `QuestionsAnswered`, `TotalScaleValue`, `AverageScaleValue`, `CompanyIdList`.
- **Controllers/ReportsController.cs**
  - `WorkExperienceReports(string? experienceRange)` and `ExportWorkExperienceReports(string? experienceRange)`:
    - Both Admin and Manager now use `GetWorkExperienceReportsAsync(null)`, so they see the **same aggregated values** across all companies.
    - Role only affects column visibility in the view (Admin sees Users + CompanyIds; Manager does not).
- **Views/Reports/WorkExperienceReports.cshtml** (new view)
  - Filter by `experienceRange` with ranges `< 5`, `5 - 10`, `10 - 15`, `15 - 20`, `> 20`.
  - Shows: `Work Experience`, `Users`, `Questions Answered`, `Sum of Scale Values`, `Average (Sum / Questions)`.
  - **Admin-only** `CompanyIds` column (from `CompanyIdList`).

## 4. Advanced Reports: CompanyId List for Admin

**Purpose:** For Admin users, show which CompanyIds contribute to each report row; hide this detail for Manager users.

**Key changes:**
- **Services/Implementations/ReportService.cs**
  - For each advanced report, added computation of `CompanyIdList` (distinct, sorted CompanyIds):
    - `GetQuestionReportsAsync()` → `QuestionReportVM.CompanyIdList`.
    - `GetFormReportsAsync()` → `FormReportVM.CompanyIdList`.
    - `GetQuestionByOUReportsAsync()` → `QuestionByOUMReportVM.CompanyIdList`.
    - `GetFormByOUReportsAsync()` → `FormByOUMReportVM.CompanyIdList`.
    - `GetAgeReportsAsync()` → `AgeReportVM.CompanyIdList`.
    - `GetWorkExperienceReportsAsync()` → `WorkExperienceReportVM.CompanyIdList`.
- **Views/Reports/FormReports.cshtml**
  - Added `isAdmin` flag from `Session["UserRole"]`.
  - New Admin-only column `CompanyIds` showing `@item.CompanyIdList`.
- **Views/Reports/QuestionReports.cshtml**
  - Same pattern: Admin-only `CompanyIds` column and cells.
- **Views/Reports/FormByOUReports.cshtml**
  - Added Admin-only `CompanyIds` column and cells.
- **Views/Reports/QuestionByOUReports.cshtml**
  - Added Admin-only `CompanyIds` column and cells.
- **Views/Reports/AgeReports.cshtml**
  - Added Admin-only `CompanyIds` column and cells.
- **Views/Reports/WorkExperienceReports.cshtml**
  - Added Admin-only `CompanyIds` column and cells.

Managers see the same numeric aggregations (responses, totals, averages) but **do not** see the CompanyIds lists.

## 5. Percentages Removed from Reports

**Purpose:** Show raw values and averages instead of percentages everywhere in reports.

**Key changes:**
- **ViewModels/Models/ReportSummaryVM.cs**
  - `AverageFormCompletionDisplay` now formatted without `%`.
- **ViewModels/Models/FormReportVM.cs, QuestionReportVM.cs, FormByOUMReportVM.cs, QuestionByOUMReportVM.cs**
  - Removed percentage-related properties (e.g., `ScaleValuePercentage`, `ScaleValuePercentageDisplay`).
- **Services/Implementations/ReportService.cs**
  - Removed `CalculateScalePercentage` and `CalculateFormScalePercentage` helpers.
  - Stopped computing any percentage-based scores.
- **Views/Reports/FormReports.cshtml, QuestionReports.cshtml, FormByOUReports.cshtml**
  - Removed "Percentage" columns and badges.
- **Views/Reports/Index.cshtml**
  - Form completion summary now shows numeric value (0–100) without `%` and label updated to "Form Completion (0-100 scale)".
- **Views/Admin/Index.cshtml, Views/Manager/Index.cshtml**
  - Dashboard completion cards now show numeric 0–100 without `%` and label text adjusted.

## Logout Button Behavior

(see previous sections for details)

## User Management (Admin)

New admin-only user management features have been added:

- **View all users**: `AdminController.ManageUsers` now uses `IUserService.GetAllUsersAsync` and `Views/Admin/ManageUsers.cshtml` to display a table of all users, including CompanyId, OU/OU2, role, age, work experience, and active status.
- **Create user**: `AdminController.CreateUser` (GET/POST) plus `Views/Admin/CreateUser.cshtml` allow creating new users (currently as `Employee` role by default).
- **Edit user**: `AdminController.EditUser` (GET/POST) and `Views/Admin/EditUser.cshtml` allow editing CompanyId, FullName, OU/OU2, Age, WorkExperience, and the active checkbox.
- **Activate / Deactivate**: `AdminController.ToggleUserActive` and the actions in `ManageUsers.cshtml` toggle the new `User.IsActive` property.
- **Reset password**: `AdminController.ResetUserPassword` (GET/POST) and `Views/Admin/ResetUserPassword.cshtml` allow an admin to set a new password for a user.

Service / repository additions:

- `GlasAnketa.Domain/Models/User.cs`: added `IsActive` boolean property (default `true`).
- `GlasAnketa.ViewModels/Models/UserVM.cs`: extended with `Age`, `WorkExperience`, and `IsActive`.
- `GlasAnketa.DataAccess/Interfaces/IUserRepository.cs`: added `GetAllWithRolesAsync` and `GetByIdWithRoleAsync`.
- `GlasAnketa.DataAccess/Implementations/UserRepository.cs`: implemented the above, and `AuthenticateAsync` now only returns active users (`IsActive == true`).
- `GlasAnketa.Services/Interfaces/IUserService.cs`: added methods for listing, editing, toggling active, and resetting passwords.
- `GlasAnketa.Services/Implementations/UserService.cs`: implements `GetAllUsersAsync`, `GetUserForEditAsync`, `UpdateUserAsync`, `ToggleUserActiveAsync`, `ResetPasswordAsync`.

### EF Core migration note

A new `IsActive` column has been added to the `User` entity. You will need to generate and apply a migration so the database schema matches the code:

```bash
# from the solution root
# generate migration
 dotnet ef migrations add AddUserIsActive --project GlasAnketa.DataAccess --startup-project GlasAnketa

# apply migration
 dotnet ef database update --project GlasAnketa.DataAccess --startup-project GlasAnketa
```

If you are not using EF Core migrations, add a nullable/bit `IsActive` column to the `Users` table manually (default `1` for existing users).

**Purpose:** Ensure Admin and Manager logout buttons perform a POST and return to the Login page.

**Key changes:**
- **Controllers/AccountController.cs**
  - `Logout` is `[HttpPost]` with `[ValidateAntiForgeryToken]`, clears session, and redirects to `Login`.
- **Views/Admin/Index.cshtml**
  - Replaced logout `<a>` link with a POST `<form>`:
    - `<form asp-controller="Account" asp-action="Logout" method="post" class="d-inline">` + antiforgery token.
- **Views/Manager/Index.cshtml**
  - Same POST form pattern used for logout.

## 7. Reports Dashboard & Linking

**Purpose:** Make Age and WorkExperience reports accessible and correctly described.

**Key changes:**
- **Views/Reports/Index.cshtml**
  - Updated Age Reports description to "Responses and averages grouped by age buckets".
  - Added new card/link for Work Experience Reports:
    - "Responses and averages grouped by work experience ranges" → `@Url.Action("WorkExperienceReports")`.

---

### Summary of Modified Files

**C# files:**
- `Controllers/AccountController.cs`
- `Controllers/ReportsController.cs`
- `Services/Interfaces/IUserService.cs`
- `Services/Interfaces/IReportService.cs`
- `Services/Implementations/UserService.cs`
- `Services/Implementations/ReportService.cs`
- `ViewModels/Models/ReportSummaryVM.cs`
- `ViewModels/Models/FormReportVM.cs`
- `ViewModels/Models/QuestionReportVM.cs`
- `ViewModels/Models/FormByOUMReportVM.cs`
- `ViewModels/Models/QuestionByOUMReportVM.cs`
- `ViewModels/Models/AgeReportVM.cs`
- `ViewModels/Models/WorkExperienceReportVM.cs`

**Razor view files (.cshtml):**
- `Views/Account/Login.cshtml` (login error modal behavior already present; used TempData error key)
- `Views/Admin/Index.cshtml`
- `Views/Manager/Index.cshtml`
- `Views/Reports/Index.cshtml`
- `Views/Reports/AgeReports.cshtml`
- `Views/Reports/WorkExperienceReports.cshtml`
- `Views/Reports/FormReports.cshtml`
- `Views/Reports/QuestionReports.cshtml`
- `Views/Reports/FormByOUReports.cshtml`
- `Views/Reports/QuestionByOUReports.cshtml`

---

## 8. QuestionReportByFilters Fixes (January 20, 2025)

**Purpose:** Fix parameter binding and implement correct OU-based filtering with OU2 grouping.

### 8.1 Parameter Name Mismatch Fix

**Problem:** Form input used `name="companyId"` but controller parameter was `levcompanyId`, causing filters to not work.

**Key changes:**
- **Views/Reports/QuestionReportByFilters.cshtml**
  - Changed form input from `name="companyId"` to `name="levcompanyId"` (line 23) to match controller parameter.

### 8.2 ReportUser Data Update

**Problem:** Original ReportUser LevCompanyIds (4420621, 4420975) had no corresponding data in the database.

**Key changes:**
- **Database: ReportUsers table**
  - Updated ReportUser 1: LevCompanyId from 4420621 to **20708** (Projects and IT, 42 answers)
  - Updated ReportUser 1: Password from "20621" to **"20708"**
  - Updated ReportUser 2: LevCompanyId from 4420975 to **20724** (HR, 38 answers)
  - Updated ReportUser 2: Password from "20975" to **"20724"**

### 8.3 OU-Based Filtering with OU2 Grouping

**Problem:** When filtering by CompanyId, report only showed data for that specific CompanyId. Requirements specified showing ALL users in the same OU, grouped by OU2.

**Example:** Filtering by CompanyId 20724 (OU="HR") should show:
- All answers from ALL users where OU="HR" (not just CompanyId 20724)
- Grouped by different OU2 values within that OU
- Multiple rows per question (one for each OU2)

**Key changes:**
- **Services/Implementations/ReportService.cs**
  - `GetQuestionReportByFiltersAsync` method completely rewritten:
    - When `companyId` is provided, looks up the user's OU from the Users table
    - Filters ALL answers by that OU (not just by CompanyId)
    - Groups results by OU2 within that OU
    - Creates multiple report rows per question (one per OU2 group)
    - Each row shows:
      - `OU`: The OU of the filtered CompanyId
      - `OU2`: Each distinct OU2 value within that OU
      - `TotalScaleValue`: Sum of scale values from all users in that OU+OU2 combination
      - `AverageScaleValue`: Average scale value from all users in that OU+OU2 combination
      - `AverageAge`, `AverageWorkExperience`: Averages from users in that OU+OU2 combination
    - Fixed average calculation: Changed from `totalScale / totalResponses` to `scaleAnswers.Average()` for correct scale-only averaging

**Behavior:**
- **Filter by CompanyId only**: Shows all OU2 groups within that CompanyId's OU
- **Filter by OU only**: Shows single row per question with all answers in that OU
- **Filter by OU2 only**: Shows single row per question with all answers in that OU2
- **Filter by CompanyId + OU2**: Shows single row per question with that specific OU+OU2 combination

### Testing

To test the updated functionality:
1. Login with CompanyId: **20708**, Password: **20708** (Admin role, Projects and IT)
2. Login with CompanyId: **20724**, Password: **20724** (Viewer role, HR)
3. Navigate to Reports → Question Reports (Filtered)
4. Apply filter with CompanyId 20724 to see all HR users grouped by OU2
