using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.DTO.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
