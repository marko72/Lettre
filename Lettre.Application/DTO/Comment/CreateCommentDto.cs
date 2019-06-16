using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.DTO.Comment
{
    public class CreateCommentDto
    {
        [Required(ErrorMessage = "Neophpodno je izabrati vest koju želite da komentarišete")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Neko mora komentarisati vest")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Sadržaj kojim želite komentarisati vest je neophodan")]
        public string Content { get; set; }
    }
}
