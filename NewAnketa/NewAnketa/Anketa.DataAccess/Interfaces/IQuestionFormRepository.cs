using Anketa.Domain.Entities;

namespace Anketa.DataAccess.Interfaces
{
    public interface IQuestionFormRepository : IRepository<QuestionForm>
    {
        Task<List<QuestionForm>> GetAllFormQuestionsAsync();
        Task<QuestionForm> GetQuestionFormByIdAsync(int id);
        Task<QuestionForm?> GetNextActiveFormAsync(int currentFormId);
        Task<QuestionForm?> GetPreviousActiveFormAsync(int currentFormId);
        Task<bool> ToggleFormStatusAsync(int formId, bool isActive);
    }
}
