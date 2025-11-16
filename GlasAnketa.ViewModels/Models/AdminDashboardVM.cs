namespace GlasAnketa.ViewModels.Models
{
    public class UserAnswerStatusVM
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool HasAnswers { get; set; }
    }

    public class AdminDashboardVM
    {
        public List<UserAnswerStatusVM> UserAnswerStatuses { get; set; } = new List<UserAnswerStatusVM>();
    }
}
