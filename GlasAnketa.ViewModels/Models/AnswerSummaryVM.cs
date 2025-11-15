namespace GlasAnketa.ViewModels.Models
{
    public class AnswerSummaryVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }

        /// <summary>
        /// Average scale value for this question (sum of scale values / number of scale responses).
        /// </summary>
        public double AverageScaleValue { get; set; }

        /// <summary>
        /// Number of responses for this question.
        /// </summary>
        public int ResponseCount { get; set; }

        /// <summary>
        /// Distribution of scale values: key = scale (1-10), value = count.
        /// </summary>
        public Dictionary<int, int> ScaleValueDistribution { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// Collected text responses (for text questions).
        /// </summary>
        public List<string> TextResponses { get; set; } = new List<string>();
    }
}
