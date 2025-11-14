namespace GlasAnketa.ViewModels.Models
{
    public class AgeReportVM
    {
        public string AgeRange { get; set; }
        public int? CompanyId { get; set; }
        public int TotalResponses { get; set; }
        public double AverageScaleValue { get; set; }
        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}