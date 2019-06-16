using Lettre.Application.Commands.Role;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.EfCommands.RoleCommands
{
    public class EfDeleteRoleCommand : EfBaseCommand, IDeleteRoleCommand
    {
        public EfDeleteRoleCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var role = Context.Roles.Find(id);
            if (role == null || role.IsDeleted == true)
            {
                throw new EntityNotFoundException();
            }
            role.IsDeleted = true;
            role.Users.Select(usr => usr.IsDeleted == true);
            Context.SaveChanges();

        }
    }
}
