using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.DTO.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        [MaxLength(20, ErrorMessage = "Too many charachters for name, 30 is max!")]
        [MinLength(3, ErrorMessage = "Too less charachters for name, 3 is min!")]
        public string Name { get; set; }
        [MaxLength(30, ErrorMessage = "Too many charachters for surname, 30 is max!")]
        [MinLength(3, ErrorMessage = "Too less charachters for surname, 3 is min!")]
        public string Surname { get; set; }
        [EmailAddress(ErrorMessage ="Email adresa nije validna")]
        public string Email { get; set; }
        [MaxLength(30, ErrorMessage = "Too many charachters for Password, 30 is max!")]
        [MinLength(3, ErrorMessage = "Too less charachters for Password, 3 is min!")]
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
