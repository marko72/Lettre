using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.DTO.Role
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage = "Neophodno je uneti naziv uloge")]
        [MaxLength(20, ErrorMessage = "Uloga može imati maksimalno 20 karaktera!")]
        [MinLength(3, ErrorMessage = "Minimalna dužina uloge je 3 karaktera!")]
        public string Name { get; set; }
    }
}
