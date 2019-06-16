using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Lettre.Api.Models
{
    public class ApiPostDto
    {
        public string Title { get; set; }
        [MinLength(10, ErrorMessage = "Vest mora sadrzati tekst duzine barem 10 karaktera")]
        public string Content { get; set; }
        [RegularExpression("^[1-9]{1,}$", ErrorMessage = "Morate proslediti kategoriju kojoj vest pripada")]
        public int CategoryId { get; set; }
        public IFormFile Picture { get; set; }
    }
}
