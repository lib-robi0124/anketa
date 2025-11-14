namespace GlasAnketa.ViewModels.Models
{
    public class QuestionReportVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionFormId { get; set; }
        public string FormTitle { get; set; }
        public int TotalResponses { get; set; }
        public int TotalScaleValue { get; set; }
        public double AverageScaleValue { get; set; }
        public double ScaleValuePercentage { get; set; }
        public string ScaleValuePercentageDisplay => $"{ScaleValuePercentage:F1}%";
        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}
