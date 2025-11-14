namespace GlasAnketa.ViewModels.Models
{
    public class ReportSummaryVM
    {
        public int TotalUsers { get; set; }
        public int TotalForms { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalAnswers { get; set; }
        public int TotalScaleAnswers { get; set; }
        public int TotalTextAnswers { get; set; }
        public double AverageFormCompletion { get; set; }
        public string AverageFormCompletionDisplay => $"{AverageFormCompletion:F1}%";
    }
}
