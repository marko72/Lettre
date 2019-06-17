using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.User;
using Lettre.Application.DTO.User;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;

namespace Lettre.EfCommands.UserCommand
{
    public class EfUpdateUserCommand : EfBaseCommand, IUpdateUserCommand
    {
        public EfUpdateUserCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(UpdateUserDto request)
        {
            if (request.Id <= 0)
            {
                throw new InvalidValueForwardedException("Morate proslediti ispravnu vrednost za korisnika kog želite izmeniti");
            }
            var user = Context.Users.Find(request.Id);

            if (user == null || user.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik kog zelite da promenite");
            }

            if (!String.IsNullOrEmpty(request.Email))
            {
                if(user.Email != request.Email)
                {
                    if (Context.Users.Any(u => u.Name == request.Name))
                    {
                        throw new EntityAlreadyExistException("Email koji zelite dodeliti korisniku");
                    }
                    user.Email = request.Email;
                }   
            }

            if(!(request.RoleId <= 0))
            {
                var role = Context.Roles.Find(request.RoleId);
                if (role == null || role.IsDeleted == true)
                {
                    throw new EntityNotFoundException("Uloga koju korisniku zelite da dodelite");
                }
                user.RoleId = request.RoleId;
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                if(request.Password != user.Password)
                {
                    user.Password = request.Password;
                }
                
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                if(user.Name != request.Name)
                {
                    user.Name = request.Name;
                }                
            }

            if (!string.IsNullOrEmpty(request.Surname))
            {
                if (user.Surname != request.Surname)
                {
                    user.Surname = request.Surname;
                }
            }

            user.ModifiedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
