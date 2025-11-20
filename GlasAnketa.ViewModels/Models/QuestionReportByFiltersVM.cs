namespace GlasAnketa.ViewModels.Models
{
    public class QuestionReportByFiltersVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionFormId { get; set; }
        public string FormTitle { get; set; }
        public int TotalResponses { get; set; }
        public int TotalScaleValue { get; set; }
        public double AverageScaleValue { get; set; }
        public int TextResponseCount { get; set; }
        public string? SampleTextResponses { get; set; }
        public int? CompanyId { get; set; }
        public string? OU { get; set; }
        public string? OU2 { get; set; }
        public double AverageAge { get; set; }
        public double AverageWorkExperience { get; set; }
        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}
