using Anketa.Domain.Entities;

namespace Anketa.DataAccess.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
       Task<List<Question>> GetByUserIdAsync (int userId);
       Task<QuestionForm> GetFormWithQuestionsAsync(int formId);
    }
}
