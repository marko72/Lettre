using Lettre.Application.DTO.Role;
using Lettre.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Role
{
    public interface IGetRoleCommand : ICommand<int, GetRoleDto>
    {
    }
}
