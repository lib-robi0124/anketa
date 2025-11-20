namespace GlasAnketa.Domain.Models
{
    public class ReportRole
    {
        public int Id { get; set; }
        public string Name { get; set; } // "Admin", "OUManager", "Viewer", etc.
        public string Description { get; set; }

        // Permission flags
        public bool CanViewAll { get; set; } // Level 0 - view everything
        public bool CanViewSubordinate { get; set; } // Level 1 - view level-1
        public bool CanViewSpecificOU { get; set; } // Level 2 - view defined OU/OU2

        public ICollection<ReportUser> Users { get; set; }
    }
}
