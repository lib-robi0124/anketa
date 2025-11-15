namespace GlasAnketa.ViewModels.Models
{
    public class QuestionByOUMReportVM
    {
        public string OU { get; set; }
        public string OU2 { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int TotalResponses { get; set; }
        public int TotalScaleValue { get; set; }
        public double AverageScaleValue { get; set; }

        // Comma-separated list of distinct CompanyIds contributing to this OU/question
        public string? CompanyIdList { get; set; }

        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}
