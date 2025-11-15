using GlasAnketa.ViewModels.Models;

namespace GlasAnketa.Services.Interfaces
{
    public interface IUserService
    {
        // Existing methods
        Task<UserVM> RegisterUser(RegisterUserVM registerUserVM, RoleVM roleVM);
        Task<QuestionVM> RegisterQuestion(RegisterQuestionVM registerQuestionVM, RoleVM roleVM);
        Task<QuestionFormVM> CreateQuestionForm(CreateQuestionFormVM createQuestionFormVM, RoleVM roleVM);
        Task<QuestionFormVM> UpdateFormAsync(QuestionFormVM formVm, RoleVM roleVM);
        Task<UserVM> GetUserById(int id);
        Task<UserVM?> ValidateUser(UserCredentialsVM userCredentialsVM);

        // User management
        Task<List<UserVM>> GetAllUsersAsync();
        Task<UserVM?> GetUserForEditAsync(int id);
        Task<bool> UpdateUserAsync(UserVM userVm);
        Task<bool> ToggleUserActiveAsync(int userId, bool isActive);
        Task<bool> ResetPasswordAsync(int userId, string newPassword);
    }
}
