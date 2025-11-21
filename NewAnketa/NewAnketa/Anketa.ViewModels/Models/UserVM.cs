using Anketa.Domain.Enums;

namespace Anketa.ViewModels.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Sector Sector { get; set; }
        public string Department { get; set; } // Organizational Unit
        public string Line { get; set; } // Secondary Level Organizational Unit
        public bool IsActive { get; set; } = true;
        public Gender Gender { get; set; }
        public PositionType PositionType { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public int Age { get; set; }
        public int WorkExperience { get; set; }
        public RoleVM? Role { get; set; }
    }
}
