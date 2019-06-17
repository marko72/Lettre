using Lettre.Application.Commands.Role;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.RoleCommands
{
    public class EfDeleteRoleCommand : EfBaseCommand, IDeleteRoleCommand
    {
        public EfDeleteRoleCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var role = Context.Roles
                .Include(r => r.Users)
                .ThenInclude(usrs => usrs.Comments)
                .Where(r => r.Id == id)
                .First();
            if (role == null || role.IsDeleted == true)
            {
                throw new EntityNotFoundException();
            }
            role.IsDeleted = true;
            var users = role.Users;
            foreach (var u in users)
            {
                if (u.IsDeleted == true)
                    continue;
                u.IsDeleted = true;
                var comms = u.Comments;
                foreach (var c in comms)
                {
                    if (c.IsDeleted == true)
                        continue;
                    c.IsDeleted = true;
                }
            }
            Context.SaveChanges();

        }
    }
}
