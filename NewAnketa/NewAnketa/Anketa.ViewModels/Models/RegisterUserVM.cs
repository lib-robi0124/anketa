using Anketa.Domain.Enums;

namespace Anketa.ViewModels.Models
{
    public class RegisterUserVM
    {
        public int CompanyId { get; set; }
        public Sector Sector { get; set; }
        public string Department { get; set; } // Organizational Unit
        public string Line { get; set; } // Secondary Level Organizational Unit
        public string Password { get; set; }
    }
}
