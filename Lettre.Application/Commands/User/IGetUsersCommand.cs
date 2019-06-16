using Lettre.Application.DTO.User;
using Lettre.Application.Interfaces;
using Lettre.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.User
{
    public interface IGetUsersCommand : ICommand<UserSearch, IEnumerable<GetUserDto>>
    {
    }
}
