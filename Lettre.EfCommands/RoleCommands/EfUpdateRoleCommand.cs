using Lettre.Application.Commands.Role;
using Lettre.Application.DTO.Role;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.RoleCommands
{
    public class EfUpdateRoleCommand : EfBaseCommand, IUpdateRoleCommand
    {
        public EfUpdateRoleCommand(LettreDbContext context) : base(context)
        {
        }


        public void Execute(GetRoleDto request)
        {
            var role = Context.Roles.Find(request.Id);
            if(role == null || role.IsDeleted == true)
            {
                throw new EntityNotFoundException("Uloga");
            }
            if (Context.Roles.Any(r => r.Name == request.Name))
            {
                throw new EntityAlreadyExistException("Uloga sa tim nazivom");
            }

            role.Name = request.Name;
            role.ModifiedAt = DateTime.Now;

            Context.SaveChanges();
        }
    }
}
