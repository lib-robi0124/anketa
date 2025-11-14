namespace GlasAnketa.ViewModels.Models
{
    public class FormByOUMReportVM
    {
        public string OU { get; set; }
        public string OU2 { get; set; }
        public int FormId { get; set; }
        public string FormTitle { get; set; }
        public int TotalResponses { get; set; }
        public int TotalScaleValue { get; set; }
        public double AverageScaleValue { get; set; }
        public double ScaleValuePercentage { get; set; }
        public string ScaleValuePercentageDisplay => $"{ScaleValuePercentage:F1}%";
        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}
