using Lettre.Application.DTO.Role;
using Lettre.Application.Interfaces;
using Lettre.Application.Searches;
using System.Collections.Generic;

namespace Lettre.Application.Commands.Role
{
    public interface IGetRolesCommand : ICommand<RoleSearch, IEnumerable<GetRoleDto>>
    {
    }
}
