using Lettre.Application.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.Searches
{
    public class UserSearch : Pagination
    {
        [MaxLength(20, ErrorMessage = "Too many charachters for name, 30 is max!")]
        [MinLength(3, ErrorMessage = "Too less charachters for name, 3 is min!")]
        public string Name { get; set; }
        [MaxLength(30, ErrorMessage = "Too many charachters for surname, 30 is max!")]
        [MinLength(3, ErrorMessage = "Too less charachters for surname, 3 is min!")]
        public string Surname { get; set; }
        [MaxLength(50, ErrorMessage = "Too many charachters for Email, 30 is max!")]
        [MinLength(3, ErrorMessage = "Too less charachters for Email, 3 is min!")]
        [RegularExpression("^.@.$", ErrorMessage ="Type of email is no good")]
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
