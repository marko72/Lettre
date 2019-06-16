using System.Collections;
using System.Collections.Generic;

namespace Lettre.Domain
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
