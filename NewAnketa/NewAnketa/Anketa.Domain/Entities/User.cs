using Anketa.Domain.Enums;

namespace Anketa.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        // Organizational Structure
        public Sector Sector { get; set; }
        public string Department { get; set; } // Organizational Unit
        public string Line { get; set; } // Secondary Level Organizational Unit
        // Authentication / Identity
        public string Password { get; set; }
        // Role information
        public int RoleId { get; set; }
        public Role Role { get; set; }
        // Audit & status
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        // Demographics
        public Gender Gender { get; set; }
        public PositionType PositionType { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public int Age { get; set; }
        public int WorkExperience { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public User()
        {
            Answers = new HashSet<Answer>();
        }
    }
}
