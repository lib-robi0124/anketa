# Project Changes Documentation

## Overview
This document summarizes all the changes made to the GlasAnketa project, including authorization fixes and UI improvements.

## Authorization Fix for "Advance Report" Functionality

### Problem
The "Advance report" functionality was inaccessible due to an authorization mismatch. The `[Authorize(Roles = "Administrator")]` attribute was using ASP.NET Core Identity claims-based authorization, while the application uses session-based authentication.

### Solution Implemented
1. **Removed** the `[Authorize(Roles = "Administrator")]` attribute from the Reports controller
2. **Added** session-based authorization methods:
   - `IsUserAdministrator()` - Checks if the current user has administrator role
   - `RedirectToLoginIfNotAdmin()` - Redirects to login if user is not an admin
3. **Applied** role checking to all report actions:
   - `Index()` - Reports dashboard
   - `QuestionReports()` - Detailed question reports
   - `FormReports()` - Detailed form reports
   - `QuestionByOUReports()` - Question reports by organizational unit
   - `FormByOUReports()` - Form reports by organizational unit

### Benefits
- ✅ Maintains security by checking user roles
- ✅ Uses existing session-based authentication system
- ✅ Provides proper feedback for unauthorized access
- ✅ Preserves user experience with redirect after login

## Back Button Addition to Reports Views

### Changes Made
Added consistent "Back" navigation to all report views with proper styling.

#### 1. Reports Index Page (`/Reports/Index`)
- **Added**: Back button linking to `/Admin/Index`
- **Location**: Top of page, above "Reports Dashboard" heading
- **Styling**: New CSS classes `.reports-header-with-back` and `.btn-back`

#### 2. Question Reports Page (`/Reports/QuestionReports`)
- **Added**: Back button linking to `/Reports/Index`
- **Location**: In header alongside report title and description
- **Styling**: `.header-content` container with `.btn-back` class
- **Fixed**: CSS conflict with duplicate `.report-header` rules

#### 3. Form Reports Page (`/Reports/FormReports`)
- **Added**: Back button linking to `/Reports/Index`
- **Location**: In header alongside report title and description
- **Styling**: `.header-content` container with `.btn-back` class
- **Fixed**: CSS conflict with duplicate `.report-header` rules

#### 4. Question by OU Reports Page (`/Reports/QuestionByOUReports`)
- **Added**: Back button linking to `/Reports/Index`
- **Location**: In header alongside report title and description
- **Styling**: `.header-content` container with `.btn-back` class

#### 5. Form by OU Reports Page (`/Reports/FormByOUReports`)
- **Added**: Back button linking to `/Reports/Index`
- **Location**: In header alongside report title and description
- **Styling**: `.header-content` container with `.btn-back` class

### CSS Styling Added
```css
/* Header with back button layout */
.reports-header-with-back {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
}

.header-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
}

/* Back button styling */
.btn-back {
    background-color: #6c757d;
    color: white;
    padding: 8px 16px;
    text-decoration: none;
    border-radius: 4px;
    border: none;
    cursor: pointer;
    font-size: 14px;
    transition: background-color 0.3s;
}

.btn-back:hover {
    background-color: #5a6268;
    color: white;
    text-decoration: none;
}
```

### CSS Issues Fixed
- **Consolidated duplicate CSS rules** in multiple report views
- **Removed conflicting `.report-header` declarations** that were hiding buttons
- **Ensured consistent styling** across all report pages

## Files Modified

### Controllers
- `GlasAnketa/Controllers/ReportsController.cs` - Authorization logic fixes

### Views
- `GlasAnketa/Views/Reports/Index.cshtml` - Added back button to admin
- `GlasAnketa/Views/Reports/QuestionReports.cshtml` - Added back button + CSS fix
- `GlasAnketa/Views/Reports/FormReports.cshtml` - Added back button + CSS fix
- `GlasAnketa/Views/Reports/QuestionByOUReports.cshtml` - Added back button
- `GlasAnketa/Views/Reports/FormByOUReports.cshtml` - Added back button

## Testing Results

### Authorization Fix
- ✅ Application builds successfully
- ✅ Reports accessible to administrators
- ✅ Unauthorized users redirected to login
- ✅ Proper role checking implemented

### Back Button Functionality
- ✅ All back buttons visible and properly styled
- ✅ Navigation works correctly:
  - Reports Index → Admin Dashboard
  - Individual Reports → Reports Index
- ✅ Consistent styling across all pages
- ✅ No browser errors or console issues

## Application Status
- **Status**: Running successfully
- **URL**: http://localhost:5041
- **Environment**: Development
- **Build**: Successful with warnings (related to Exchange.WebServices package compatibility)

## Next Steps
The application is now fully functional with:
- ✅ Proper authorization for report access
- ✅ Complete navigation with back buttons
- ✅ Consistent UI/UX across report views
- ✅ No blocking issues

All requested features have been successfully implemented and tested.

## User Management Fixes (2025-11-15)

### Context
An admin user management feature was added to allow viewing, creating, editing, activating/deactivating users, and resetting passwords. After the initial implementation, two issues were reported:

1. Admin users could not log in after introducing the `IsActive` flag (database column missing).
2. Editing a user in **Manage Users** produced a "Role is required" error and the role dropdown always defaulted to **Administrator**.

### Fixes Implemented

1. **Database schema update for `IsActive`**
   - Added `IsActive` bit column to the `Users` table with default `1` for existing rows.
   - Example SQL used:
     ```sql
     ALTER TABLE [dbo].[Users]
     ADD [IsActive] bit NOT NULL
         CONSTRAINT DF_Users_IsActive DEFAULT (1) WITH VALUES;
     ```
   - This aligns the database with `GlasAnketa.Domain/Models/User.cs` and `UserRepository.AuthenticateAsync`, which now filters by `u.IsActive`.

2. **Edit User: Role dropdown and validation**
   - **Correct role mapping**
     - Updated `GlasAnketa.Services/AutoMappers/RoleMappingProfile.cs` so that:
       - `Role.Id` ⇄ `RoleVM.RoleId`.
       - `Role.Name` ⇄ `RoleVM.Name`.
     - This ensures `UserVM.Role.RoleId` reflects the actual role (Administrator / Employee / Manager) when loading the edit form.
   - **Fix dropdown selection**
     - `AdminController.EditUser (GET)` now builds `ViewBag.Roles` as a list of `SelectListItem` with the correct item marked `Selected` based on `user.Role?.RoleId`.
     - `Views/Admin/EditUser.cshtml` uses `asp-items="(IEnumerable<SelectListItem>)ViewBag.Roles"` directly, so the `Selected` flags are honored and the dropdown shows the current role instead of always Administrator.
   - **Resolve "Role is required" on Save**
     - `UserVM.Role` was treated as a complex bound property; on POST, MVC attempted to validate it even though only a `selectedRoleId` integer is posted.
     - `UserVM.Role` was made nullable and is now set explicitly in the POST:
       - `AdminController.EditUser(UserVM model, int selectedRoleId)` constructs `model.Role = new RoleVM { RoleId = selectedRoleId, Name = ... }` before calling `UserService.UpdateUserAsync`.
     - This removes the spurious "Role is required" validation error while still correctly updating the user's role.
   - **Persist Active flag on save**
     - `GlasAnketa.Services/Implementations/UserService.cs` (`UpdateUserAsync`) now copies the `IsActive` flag from `UserVM` to the domain `User` entity:
       - `existing.IsActive = userVm.IsActive;`

### Result
- Admins can log in again after the `IsActive` column is added.
- The **Edit User** form shows the correct current role (Administrator/Employee/Manager).
- Changing Role and WorkExperience and clicking **Save** updates the user successfully without validation errors.
- The `Active` checkbox and `User.IsActive` flag now stay in sync.

## Manager Dashboard Management Shortcuts (2025-11-15)

### Requirement
Provide Manager users with access to the same management functionality as Admin where appropriate:
- Manage Questions
- Manage Forms
- Manage Users, but only for **adding** new users (no edit/deactivate/reset).

### Implementation

1. **ManagerController shortcuts**
   - Updated `GlasAnketa/Controllers/ManagerController.cs`:
     - `ManageQuestions()` → redirects to `AdminController.ManageQuestions` (full questions UI).
     - `ManageForms()` → redirects to `AdminController.ManageForms` (full forms UI).
     - `ManageUsers()` → redirects directly to `AdminController.CreateUser` so Managers can only add new users.

2. **Manager dashboard UI**
   - Updated `GlasAnketa/Views/Manager/Index.cshtml` in the "Reports" section:
     - Renamed section to **"Management & Reports"**.
     - Added three management cards:
       - **Manage Questions** → `@Url.Action("ManageQuestions", "Manager")`.
       - **Manage Forms** → `@Url.Action("ManageForms", "Manager")`.
       - **Manage Users** → `@Url.Action("ManageUsers", "Manager")`, with description "Create new users (no edit/delete)".

### Result
- Managers have quick access from their dashboard to manage questions and forms using the same Admin UIs.
- Managers can initiate **Create User** via the Manager dashboard but are not exposed to the full Manage Users list or edit/deactivate/reset operations.

# Changes Made
Date: 2025-11-14

## 1) Login: Wrong Credentials Popup
- Behavior: When login fails, a popup shows “wrong user or pass” with an OK button that returns to the login screen.
- Implementation:
  - Set error and redirect: `GlasAnketa/Controllers/AccountController.cs:28–33` now sets `TempData["LoginError"] = "wrong user or pass"` and redirects to `Login`.
  - Modal on login view: `GlasAnketa/Views/Account/Login.cshtml` includes a Bootstrap modal that triggers if `TempData["LoginError"]` is present; the OK button navigates to the login page.

## 2) Age Reports: Response and Average Fixed
- Data source corrected to use `Answer.Age` (matching updated `Answers` table) instead of `User.Age`.
- Calculation:
  - Response: Count of answers with `ScaleValue` for the logged `CompanyId` within the selected age bucket.
  - Average: Sum of `ScaleValue` for those answers divided by Response.
- Implementation:
  - Bucket filter: `GlasAnketa.Services/Implementations/ReportService.cs:296–300` now filters using `a.Age`.
  - Controller action remains: `GlasAnketa/Controllers/ReportsController.cs:160–183`.

## 3) Work Experience Reports (New)
- Added report with buckets: `< 5`, `5 - 10`, `10 - 15`, `15 - 20`, `> 20`.
- Calculation mirrors Age Reports:
  - Response: Count of answers with `ScaleValue` for the logged `CompanyId` within the work experience bucket.
  - Average: Sum of `ScaleValue` divided by Response.
- Implementation:
  - Service method: `GetWorkExperienceReportsAsync(int? companyId)` in `GlasAnketa.Services/Implementations/ReportService.cs` (inserted before `CalculateScalePercentage`).
  - Controller action: `WorkExperienceReports(string? experienceRange)` in `GlasAnketa/Controllers/ReportsController.cs:160` (new action returns `WorkExperienceReports` view).
  - New view: `GlasAnketa/Views/Reports/WorkExperienceReports.cshtml` (lists Experience Range, Responses, Average; no percentages).

## 4) Admin Logout Returns to Login
- Changed logout button to use POST with anti-forgery token; clears session and redirects to login.
- Implementation:
  - View update: `GlasAnketa/Views/Admin/Index.cshtml:28–31` replaced link with form posting to `Account/Logout`.
  - Action used: `GlasAnketa/Controllers/AccountController.cs:58–64` (`[HttpPost]`, `[ValidateAntiForgeryToken]`).

## 5) Removed Percentages from All Reports
- Dropped Percentage column and badges from report views:
  - `GlasAnketa/Views/Reports/QuestionReports.cshtml` (table headers/cells updated)
  - `GlasAnketa/Views/Reports/FormReports.cshtml` (table headers/cells updated)
  - `GlasAnketa/Views/Reports/QuestionByOUReports.cshtml` (table headers/cells updated)
  - `GlasAnketa/Views/Reports/FormByOUReports.cshtml` (table headers/cells updated)
- Note: Dashboard completion rate is part of the admin dashboard summary, not a report; it remains unchanged. Request update if you want it altered as well.

## Endpoints
- Age Reports: `/Reports/AgeReports`
- Work Experience Reports: `/Reports/WorkExperienceReports`
- Reports Dashboard: `/Reports/Index`
- Admin Logout: POST `/Account/Logout`
# Changes Made
Date: 2025-11-14

## 1) Login: Wrong Credentials Popup
- Behavior: When login fails, a popup shows “wrong user or pass” with an OK button that returns to the login screen.
- Implementation:
  - Set error and redirect: `GlasAnketa/Controllers/AccountController.cs:28–33` now sets `TempData["LoginError"] = "wrong user or pass"` and redirects to `Login`.
  - Modal on login view: `GlasAnketa/Views/Account/Login.cshtml` includes a Bootstrap modal that triggers if `TempData["LoginError"]` is present; the OK button navigates to the login page.

## 2) Age Reports: Response and Average Fixed
- Data source corrected to use `Answer.Age` (matching updated `Answers` table) instead of `User.Age`.
- Calculation:
  - Response: Count of answers with `ScaleValue` for the logged `CompanyId` within the selected age bucket.
  - Average: Sum of `ScaleValue` for those answers divided by Response.
- Implementation:
  - Bucket filter: `GlasAnketa.Services/Implementations/ReportService.cs:296–300` now filters using `a.Age`.
  - Controller action remains: `GlasAnketa/Controllers/ReportsController.cs:160–183`.

## 3) Work Experience Reports (New)
- Added report with buckets: `< 5`, `5 - 10`, `10 - 15`, `15 - 20`, `> 20`.
- Calculation mirrors Age Reports:
  - Response: Count of answers with `ScaleValue` for the logged `CompanyId` within the work experience bucket.
  - Average: Sum of `ScaleValue` divided by Response.
- Implementation:
  - Service method: `GetWorkExperienceReportsAsync(int? companyId)` in `GlasAnketa.Services/Implementations/ReportService.cs` (inserted before `CalculateScalePercentage`).
  - Controller action: `WorkExperienceReports(string? experienceRange)` in `GlasAnketa/Controllers/ReportsController.cs:160` (new action returns `WorkExperienceReports` view).
  - New view: `GlasAnketa/Views/Reports/WorkExperienceReports.cshtml` (lists Experience Range, Responses, Average; no percentages).

## 4) Admin Logout Returns to Login
- Changed logout button to use POST with anti-forgery token; clears session and redirects to login.
- Implementation:
  - View update: `GlasAnketa/Views/Admin/Index.cshtml:28–31` replaced link with form posting to `Account/Logout`.
  - Action used: `GlasAnketa/Controllers/AccountController.cs:58–64` (`[HttpPost]`, `[ValidateAntiForgeryToken]`).

## 5) Removed Percentages from All Reports
- Dropped Percentage column and badges from report views:
  - `GlasAnketa/Views/Reports/QuestionReports.cshtml` (table headers/cells updated)
  - `GlasAnketa/Views/Reports/FormReports.cshtml` (table headers/cells updated)
  - `GlasAnketa/Views/Reports/QuestionByOUReports.cshtml` (table headers/cells updated)
  - `GlasAnketa/Views/Reports/FormByOUReports.cshtml` (table headers/cells updated)
- Note: Dashboard completion rate is part of the admin dashboard summary, not a report; it remains unchanged. Request update if you want it altered as well.

## Endpoints
- Age Reports: `/Reports/AgeReports`
- Work Experience Reports: `/Reports/WorkExperienceReports`
- Reports Dashboard: `/Reports/Index`
- Admin Logout: POST `/Account/Logout`


## New: Age Reports by Buckets

### Overview
Added a new Age Reports page that aggregates scale-based answers into age buckets and computes cumulative percentages.

### Age Buckets
- < 25
- 25 - 30
- 31 - 40
- 41 - 50
- > 50

### Filters
- Optional `companyId` filter to restrict results to a specific company.

### Calculation Logic
- Uses existing scale percentage methodology: total scale values divided by the maximum possible (`count * 5`) to yield a percentage.

### Files Modified/Added
- Added `GlasAnketa.ViewModels/Models/AgeReportVM.cs`
- Updated `GlasAnketa.Services/Interfaces/IReportService.cs` with `GetAgeReportsAsync(int? companyId = null)`
- Updated `GlasAnketa.Services/Implementations/ReportService.cs` to implement age aggregations and percentages
- Updated `GlasAnketa/Controllers/ReportsController.cs` with `AgeReports(int? companyId)` and `ExportAgeReports(int? companyId)`
- Added `GlasAnketa/Views/Reports/AgeReports.cshtml` with filtering UI and export button
- Updated `GlasAnketa/Views/Reports/Index.cshtml` to include navigation link to Age Reports

### How to Use
- Navigate to `http://localhost:5041/Reports/AgeReports`
- To filter by company, use `http://localhost:5041/Reports/AgeReports?companyId=20621`
- Click “Export CSV” to download the current view’s data

### Status
- Built and verified in Development environment
- UI renders with back navigation and filter form
# Change Log
Date: 2025-11-14

## Summary
The following changes were applied across the project to improve login feedback and correct reporting logic:

### 1) Login: Wrong Credentials Popup
- Behavior: When login fails, a popup shows “wrong user or pass” with an OK button that returns to the login screen.
- Controller:
  - `GlasAnketa/Controllers/AccountController.cs:28–33`
  - On invalid credentials: `TempData["LoginError"] = "wrong user or pass"` and `RedirectToAction("Login", new { error = "1" })`.
- View:
  - `GlasAnketa/Views/Account/Login.cshtml:95–106`
  - Popup triggers if either `TempData["LoginError"]` or query `?error=1` is present.
  - Uses Bootstrap modal when available; falls back to native alert.
  - OK button closes modal and reloads the login page.

### 2) Age Reports: Data Correctness
- Requirement: Filter by `dbo.Answers.CompanyId` and `dbo.Answers.Age` across all answers.
- Implementation:
  - `GlasAnketa.Services/Implementations/ReportService.cs:267–325`
  - Query filters by `Answers.CompanyId` (from session) and buckets by `Answers.Age`.
  - Excludes records with `Age <= 0` to prevent invalid bucket counts.
  - Response: total count of all answers in the bucket for the company.
  - Average: sum of `ScaleValue` for answers in the bucket divided by the Response.

### 3) Work Experience Reports (Added)
- Buckets: `< 5`, `5 - 10`, `10 - 15`, `15 - 20`, `> 20`.
- Service: `GetWorkExperienceReportsAsync(int? companyId)` in `GlasAnketa.Services/Implementations/ReportService.cs`.
- Controller: `WorkExperienceReports(string? experienceRange)` in `GlasAnketa/Controllers/ReportsController.cs`.
- View: `GlasAnketa/Views/Reports/WorkExperienceReports.cshtml`.

### 4) Admin Logout: Return to Login
- Changed logout button to POST with anti-forgery token.
- View: `GlasAnketa/Views/Admin/Index.cshtml:28–31`.
- Action: `GlasAnketa/Controllers/AccountController.cs:58–64` clears session and redirects to login.

### 5) Percentages Removed from Reports
- Removed “Percentage” column from:
  - `GlasAnketa/Views/Reports/QuestionReports.cshtml`
  - `GlasAnketa/Views/Reports/FormReports.cshtml`
  - `GlasAnketa/Views/Reports/QuestionByOUReports.cshtml`
  - `GlasAnketa/Views/Reports/FormByOUReports.cshtml`

## Endpoints
- Age Reports: `/Reports/AgeReports`
- Work Experience Reports: `/Reports/WorkExperienceReports`
- Reports Dashboard: `/Reports/Index`
- Admin Logout: POST `/Account/Logout`

## Verification Steps
- Wrong login → popup appears; OK returns to login.
- Age Reports for specific `CompanyId`:
  - Buckets show non-zero `TotalResponses` when `Answers.Age > 0` exists.
  - `AverageScaleValue = Sum(ScaleValue where present) / TotalResponses`.
- Work Experience Reports show values per bucket when data exists.