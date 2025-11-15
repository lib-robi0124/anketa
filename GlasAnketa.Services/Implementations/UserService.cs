using AutoMapper;
using GlasAnketa.DataAccess.Interfaces;
using GlasAnketa.Domain.Models;
using GlasAnketa.Services.Interfaces;
using GlasAnketa.ViewModels.Models;

namespace GlasAnketa.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionFormRepository _questionFormRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IQuestionRepository questionRepository,
            IQuestionFormRepository questionFormRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _questionFormRepository = questionFormRepository;
            _mapper = mapper;
        }

        // ✅ Register a new employee or admin user
        public async Task<UserVM> RegisterUser(RegisterUserVM registerUserVM, RoleVM roleVM)
        {
            var user = _mapper.Map<User>(registerUserVM);
            user.Password = registerUserVM.Password;
            user.RoleId = roleVM.Name == "Administrator" ? 1 : 2;
            user.CreatedDate = DateTime.UtcNow;
            user.IsActive = true;

            await _userRepository.AddAsync(user);

            var createdUser = await _userRepository.GetByCompanyIdAsync(user.CompanyId);
            return _mapper.Map<UserVM>(createdUser);
        }

        // ✅ Get user by ID
        public async Task<UserVM> GetUserById(int id)
        {
            var user = await _userRepository.GetActiveAsync(id);
            return _mapper.Map<UserVM>(user);
        }

        // ✅ Validate login credentials
        public async Task<UserVM?> ValidateUser(UserCredentialsVM userCredentialsVM)
        {
            var user = await _userRepository.AuthenticateAsync(
                userCredentialsVM.CompanyId,
                userCredentialsVM.Password
            );

            if (user == null)
            {
                // Return null so the controller can handle login error gracefully
                return null;
            }

            return _mapper.Map<UserVM>(user);
        }

        // ✅ Admin creates a new question (linked to form)
        public async Task<QuestionVM> RegisterQuestion(RegisterQuestionVM registerQuestionVM, RoleVM roleVM)
        {
            var question = _mapper.Map<Question>(registerQuestionVM);
            question.UserId = registerQuestionVM.CreatedByUserId;
            question.IsRequired = true;

            await _questionRepository.AddAsync(question);
            return _mapper.Map<QuestionVM>(question);
        }

        // ✅ Admin creates a new QuestionForm with optional questions
        public async Task<QuestionFormVM> CreateQuestionForm(CreateQuestionFormVM createQuestionFormVM, RoleVM roleVM)
        {
            var questionForm = _mapper.Map<QuestionForm>(createQuestionFormVM);
            questionForm.CreatedDate = DateTime.UtcNow;
            questionForm.IsActive = true;

            await _questionFormRepository.AddAsync(questionForm);

            // Add questions if provided
            if (createQuestionFormVM.Questions?.Any() == true)
            {
                foreach (var q in createQuestionFormVM.Questions)
                {
                    var newQuestion = new Question
                    {
                        Text = q.Text,
                        QuestionFormId = questionForm.Id,
                        QuestionTypeId = q.QuestionTypeId,
                        UserId = roleVM.RoleId // Admin who creates
                    };

                    await _questionRepository.AddAsync(newQuestion);
                }
            }

            return _mapper.Map<QuestionFormVM>(questionForm);
        }

        // ✅ Update existing QuestionForm
        public async Task<QuestionFormVM> UpdateFormAsync(QuestionFormVM formVm, RoleVM roleVM)
        {
            var qForm = _mapper.Map<QuestionForm>(formVm);
            qForm.CreatedDate = DateTime.UtcNow;
            qForm.IsActive = true;

            await _questionFormRepository.Update(qForm);
            return _mapper.Map<QuestionFormVM>(qForm);
        }

        // ✅ List all users for admin management
        public async Task<List<UserVM>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllWithRolesAsync();
            return _mapper.Map<List<UserVM>>(users);
        }

        // ✅ Load single user (with role) for editing
        public async Task<UserVM?> GetUserForEditAsync(int id)
        {
            var user = await _userRepository.GetByIdWithRoleAsync(id);
            if (user == null) return null;
            return _mapper.Map<UserVM>(user);
        }

        // ✅ Update user basic info and role
        public async Task<bool> UpdateUserAsync(UserVM userVm)
        {
            var existing = await _userRepository.GetByIdWithRoleAsync(userVm.Id);
            if (existing == null) return false;

            existing.CompanyId = userVm.CompanyId;
            existing.FullName = userVm.FullName;
            existing.OU = userVm.OU;
            existing.OU2 = userVm.OU2;
            existing.Age = userVm.Age;
            existing.WorkExperience = userVm.WorkExperience;
            existing.IsActive = userVm.IsActive;

            if (userVm.Role != null && userVm.Role.RoleId > 0)
            {
                existing.RoleId = userVm.Role.RoleId;
            }

            await _userRepository.Update(existing);
            return true;
        }

        // ✅ Activate/deactivate user
        public async Task<bool> ToggleUserActiveAsync(int userId, bool isActive)
        {
            var existing = await _userRepository.GetByIdWithRoleAsync(userId);
            if (existing == null) return false;

            existing.IsActive = isActive;
            await _userRepository.Update(existing);
            return true;
        }

        // ✅ Reset user password (admin operation)
        public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
        {
            var existing = await _userRepository.GetByIdWithRoleAsync(userId);
            if (existing == null) return false;

            existing.Password = newPassword;
            await _userRepository.Update(existing);
            return true;
        }
    }

}
