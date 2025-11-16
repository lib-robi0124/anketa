namespace GlasAnketa.ViewModels.Models
{
    public class QuestionReportVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionFormId { get; set; }
        public string FormTitle { get; set; }

        /// <summary>
        /// Total number of answers for this question (scale + text).
        /// </summary>
        public int TotalResponses { get; set; }

        /// <summary>
        /// Sum of all scale values for this question.
        /// </summary>
        public int TotalScaleValue { get; set; }

        /// <summary>
        /// Average scale value (sum of scale values / number of scale responses).
        /// </summary>
        public double AverageScaleValue { get; set; }

        /// <summary>
        /// Comma-separated list of distinct CompanyIds contributing to this question.
        /// </summary>
        public string? CompanyIdList { get; set; }

        /// <summary>
        /// Number of text answers for this question.
        /// </summary>
        public int TextResponseCount { get; set; }

        /// <summary>
        /// Sample of text answers (first few, joined for display/export).
        /// </summary>
        public string? SampleTextResponses { get; set; }

        public string AverageScaleValueDisplay => $"{AverageScaleValue:F2}";
    }
}
