using Anketa.Domain.Enums;

namespace Anketa.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Sector Sector { get; set; } // enum
        public string Department { get; set; } // Organizational Unit
        public string Line { get; set; } // Secondary Level Organizational Unit
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Gender Gender { get; set; } // enum
        public PositionType PositionType { get; set; } // enum
        public EducationLevel EducationLevel { get; set; } // enum
        public int Age { get; set; }
        public int WorkExperience { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public User()
        {
            Answers = new HashSet<Answer>();
        }
    }
}
