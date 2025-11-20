namespace GlasAnketa.Domain.Models
{
    public class ReportUser
    {
        public int Id { get; set; }
        public int LevCompanyId { get; set; } // Reference to new user for reports
        public string Password { get; set; }

        // Role information with hierarchical access
        public int ReportRoleId { get; set; }
        public ReportRole ReportRole { get; set; }

        // Specific report access configuration
        public string AllowedOUs { get; set; } // CSV of allowed OUs for filtering
        public string AllowedOU2s { get; set; } // CSV of allowed secondary OUs

        // Audit & status
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
