namespace Anketa.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
        // Permission flags
        public bool CanViewAll { get; set; } // Level 0 - view everything
        public bool CanViewSector { get; set; } // Level 1 - view specified sector
        public bool CanViewDepartment { get; set; } // Level 2 - view defined department
        public bool CanViewLine { get; set; } // Level 3 - view defined line

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
