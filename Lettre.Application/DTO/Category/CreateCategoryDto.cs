using System.ComponentModel.DataAnnotations;

namespace Lettre.Application.DTO.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(ErrorMessage ="Naziv kategorije ne sme biti duzi od 20 cifara")]
        public string Name { get; set; }
    }
}
