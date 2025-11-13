using GlasAnketa.ViewModels.Models;

namespace GlasAnketa.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<QuestionReportVM>> GetQuestionReportsAsync();
        Task<List<FormReportVM>> GetFormReportsAsync();
        Task<List<QuestionByOUMReportVM>> GetQuestionByOUReportsAsync();
        Task<List<FormByOUMReportVM>> GetFormByOUReportsAsync();
        Task<ReportSummaryVM> GetReportSummaryAsync();
    }
}
