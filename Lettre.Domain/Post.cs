﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Domain
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
