using Lettre.Application.Commands.Role;
using Lettre.Application.DTO.Category;
using Lettre.Application.DTO.Role;
using Lettre.Application.Exceptions;
using Lettre.Application.Interfaces;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.RoleCommands
{
    public class EfGetRoleCommand : EfBaseCommand, IGetRoleCommand
    {
        public EfGetRoleCommand(LettreDbContext context) : base(context)
        {
        }

        public GetRoleDto Execute(int id)
        {
            var role = Context.Roles.Find(id);
            if (role == null || role.IsDeleted==true)
            {
                throw new EntityNotFoundException("Kategorija");
            }

            return new GetRoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
