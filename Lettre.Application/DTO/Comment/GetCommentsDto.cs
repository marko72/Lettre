using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.DTO.Comment
{
    public class GetCommentsDto
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
