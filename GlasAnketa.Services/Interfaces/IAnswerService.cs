using GlasAnketa.ViewModels.Models;

namespace GlasAnketa.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<bool> SubmitAnswersAsync(int userId, int formId, Dictionary<int, object> answers);
        Task<List<AnswerVM>> GetUserAnswersAsync(int userId, int formId);
        Task<List<AnswerVM>> GetFormAnswersAsync(int formId);
        Task SaveAnswersAsync(List<AnswerVM> answers);
        Task<Dictionary<int, AnswerSummaryVM>> GetAnswerSummariesAsync(int formId);
        Task<bool> ClearAnswersAsync(int userId, int formId);

        /// <summary>
        /// Returns distinct user IDs that have at least one answer recorded.
        /// </summary>
        Task<List<int>> GetUserIdsWithAnyAnswersAsync();
    }
}
