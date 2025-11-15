using System.Data;

namespace GlasAnketa.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FullName { get; set; }
        public string OU { get; set; } // Organizational Unit
        public string OU2 { get; set; } // Secondary Level Organizational Unit
        public string Password { get; set; }

        // Role information
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Audit & status
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Demographics (used in reports)
        public int Age { get; set; }
        public int WorkExperience { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public User()
        {
            Answers = new HashSet<Answer>();
        }
    }
}
