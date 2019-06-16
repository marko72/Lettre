using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Domain
{
    public class Comment : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
