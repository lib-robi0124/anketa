using Anketa.Domain.Enums;

namespace Anketa.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleType Type { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
