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
        // Display without percentage symbol as percentages are no longer shown in reports
        public string AverageFormCompletionDisplay => $"{AverageFormCompletion:F1}";
    }
}
