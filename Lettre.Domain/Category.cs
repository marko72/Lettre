using System.Collections.Generic;

namespace Lettre.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
