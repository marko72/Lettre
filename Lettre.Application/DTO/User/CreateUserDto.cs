using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.DTO.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage ="Neophodno je uneti ime korisnika")]
        [MaxLength(20, ErrorMessage = "Maksimalna dužina imena je 20 karaktera")]
        [MinLength(3, ErrorMessage = "Minimalna dužina imena je 3 karaktera!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Neophodno je uneti prezime korisnika")]
        [MaxLength(30, ErrorMessage = "Maksimalna dužina prezimena je 30 karaktera")]
        [MinLength(3, ErrorMessage = "Minimalna dužina prezimena je 3 karaktera!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Neophodno je uneti e-mail korisnika")]
        [EmailAddress(ErrorMessage = "Neispravna e-mail adresa")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Neophodno je uneti lozinku korisnika")]
        [MaxLength(30, ErrorMessage = "Lozinka može imati maksimalno 30 karaktera!")]
        [MinLength(6, ErrorMessage = "Minimalna dužina lozinke je 6 karaktera!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Neophodno je izabrati ulogu korisnika")]
        public int RoleId { get; set; }
    }
}
