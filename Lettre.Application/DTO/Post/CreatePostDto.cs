using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.DTO.Post
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "Neophodno je uneti naslov vesti")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Morate uneti sadržaj vesti")]
        [MinLength(10, ErrorMessage = "Vest mora sadrzati tekst duzine barem 10 karaktera")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Neophodno je uneti kategoriju kojoj vest pripada")]
        [RegularExpression("^[1-9]{1,}$",ErrorMessage = "Morate proslediti kategoriju kojoj vest pripada")]
        public int CategoryId { get; set; }
        public string PicturePath { get; set; }
    }
}
