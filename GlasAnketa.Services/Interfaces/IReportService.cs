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
        Task<List<AgeReportVM>> GetAgeReportsAsync(int? companyId = null);
        Task<List<WorkExperienceReportVM>> GetWorkExperienceReportsAsync(int? companyId = null);
        Task<List<QuestionReportByFiltersVM>> GetQuestionReportByFiltersAsync(int? companyId = null, string? ou = null, string? ou2 = null);
    }
}
