using Lettre.Application.Commands.Role;
using Lettre.Application.DTO.Role;
using Lettre.Application.Exceptions;
using Lettre.Domain;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.RoleCommands
{
    public class EfCreateRoleCommand : EfBaseCommand, ICreateRoleCommand
    {
        public EfCreateRoleCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(CreateRoleDto request)
        {
            if (Context.Roles.Any(r => r.Name == request.Name))
                throw new EntityAlreadyExistException("Uloga sa tim imenom");
            Context.Roles.Add(new Role
            {
                Name = request.Name,
                CreatedAt = DateTime.Now
            });
            Context.SaveChanges();
        }
    }
}
