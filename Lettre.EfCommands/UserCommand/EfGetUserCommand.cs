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
    public class EfGetUserCommand : EfBaseCommand, IGetUserCommand
    {
        public EfGetUserCommand(LettreDbContext context) : base(context)
        {
        }

        public GetUserDto Execute(int id)
        {
            var obj = Context.Users.Find(id);
            if (obj == null || obj.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik sa tim id-jem");
            }
            var roleObj = Context.Roles.Find(obj.RoleId);
            return new GetUserDto
            {
                Name = obj.Name,
                Surname = obj.Surname,
                Email = obj.Email,
                RoleName = roleObj.Name,
                Id = obj.Id
            };
        }
    }
}
