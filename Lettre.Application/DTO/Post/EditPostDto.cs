using System.ComponentModel.DataAnnotations;

namespace Lettre.Application.DTO.Post
{
    public class EditPostDto
    {
        [Required(ErrorMessage = "Neophodno je proslediti vest za izmenu")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}
