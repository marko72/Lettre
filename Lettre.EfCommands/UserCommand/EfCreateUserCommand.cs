using Lettre.Application.Commands.User;
using Lettre.Application.DTO.User;
using Lettre.Application.Exceptions;
using Lettre.Domain;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.UserCommand
{
    public class EfCreateUserCommand : EfBaseCommand, ICreateUserCommand
    {
        public EfCreateUserCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(CreateUserDto request)
        {
            var role = Context.Roles.Find(request.RoleId);
            if (role == null || role.IsDeleted == true)
            {
                throw new EntityNotFoundException("Uloga koja je dodeljena korisniku");
            }
            if (Context.Users.Any(u => u.Email == request.Email))
            {
                throw new EntityAlreadyExistException("Korisnik sa tim email-om");
            }
            Context.Users.Add(new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,
                RoleId = request.RoleId
            });
            Context.SaveChanges();
        }
    }
}
