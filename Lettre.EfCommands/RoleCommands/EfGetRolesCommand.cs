using Lettre.Application.Commands.Role;
using Lettre.Application.DTO.Role;
using Lettre.Application.Searches;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.RoleCommands
{
    public class EfGetRolesCommand : EfBaseCommand, IGetRolesCommand
    {
        public EfGetRolesCommand(LettreDbContext context) : base(context)
        {
        }

        public IEnumerable<GetRoleDto> Execute(RoleSearch request)
        {
            var roles = Context.Roles.AsQueryable();
            if(request.Name != null)
            {
                roles = roles.Where(r => r.Name.Contains(request.Name) && r.IsDeleted == false);
            }
            if(request.Id != 0)
            {
                roles = roles.Where(r => r.Id == request.Id);
            }
            return roles.Select(r => new GetRoleDto
            {
                Name = r.Name,
                Id = r.Id
            });
        }
    }
}
