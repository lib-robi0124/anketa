namespace GlasAnketa.ViewModels.Models
{
    public class FormReportVM
    {
        public int FormId { get; set; }
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public int TotalResponses { get; set; }
        public int TotalScaleValue { get; set; }
        public double AverageScaleValue { get; set; }
        public int QuestionCount { get; set; }

        // Comma-separated list of distinct CompanyIds contributing to this form
        public string? CompanyIdList { get; set; }

        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}
