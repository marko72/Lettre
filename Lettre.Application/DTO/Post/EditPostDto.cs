using System.ComponentModel.DataAnnotations;

namespace Lettre.Application.DTO.Post
{
    public class EditPostDto
    {
        [Required(ErrorMessage = "Neophodno je proslediti vest za izmenu")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Required(ErrorMessage = "Neophodno je uneti kategoriju kojoj vest pripada")]
        [RegularExpression("^[1-9]{1,}$", ErrorMessage = "Morate proslediti kategoriju kojoj vest pripada")]
        public int CategoryId { get; set; }
    }
}
