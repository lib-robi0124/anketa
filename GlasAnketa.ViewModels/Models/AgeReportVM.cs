namespace GlasAnketa.ViewModels.Models
{
    public class AgeReportVM
    {
        public string AgeRange { get; set; }
        public int? CompanyId { get; set; }

        // Number of distinct users (CompanyId) in this age bucket
        public int CountUsers { get; set; }

        // Total number of questions answered (answers with a scale value)
        public int QuestionsAnswered { get; set; }

        // Sum of all scale values in the bucket
        public int TotalScaleValue { get; set; }

        // Comma-separated list of distinct CompanyIds contributing to this age bucket
        public string? CompanyIdList { get; set; }

        // For backward compatibility, treat TotalResponses as number of users
        public int TotalResponses { get; set; }

        // Average = TotalScaleValue / QuestionsAnswered
        public double AverageScaleValue { get; set; }
        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}
