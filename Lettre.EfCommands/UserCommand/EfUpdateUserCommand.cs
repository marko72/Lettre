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
            var user = Context.Users.Find(request.Id);
            if (user == null || user.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik kog zelite da promenite");
            }
            if (user.Email == request.Email || Context.Users.Any(u => u.Name == request.Name))
            {
                throw new EntityAlreadyExistException("Email koji zelite dodeliti korisniku");
            }
            if(Context.Roles.Any(r => r.Id == request.RoleId))
            {
                throw new EntityNotFoundException("Uloga koju korisniku zelite da dodelite");
            }
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.ModifiedAt = DateTime.Now;
            user.Password = request.Password;
            user.RoleId = request.RoleId;
            user.Email = request.Email;

            Context.SaveChanges();
        }
    }
}
