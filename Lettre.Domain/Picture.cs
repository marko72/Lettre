using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Domain
{
    public class Picture : BaseEntity
    {
        public string Path { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

    }
}
