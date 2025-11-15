using GlasAnketa.Domain.Models;

namespace GlasAnketa.ViewModels.Models
{
    public class AnswerVM
    {
        public int Id { get; set; }

        // Who answered
        public int CompanyId { get; set; }
        public int UserId { get; set; }

        // What was answered
        public int QuestionId { get; set; }
        public int QuestionFormId { get; set; }

        // Answer values
        public int? ScaleValue { get; set; } // For scale-based questions, nullable if not applicable
        public string? TextValue { get; set; } // For text-based questions

        public DateTime AnsweredDate { get; set; } = DateTime.UtcNow;
    }
}
