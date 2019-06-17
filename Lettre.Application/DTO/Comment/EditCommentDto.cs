using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.DTO.Comment
{
    public class EditCommentDto
    {
        [Required(ErrorMessage ="Morate proslediti komentar koji želite izmeniti")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Sadržaj komentara ne sme biti prazan")]
        public string Content { get; set; }
    }
}
