using Lettre.Application.DTO.User;
using Lettre.Application.Interfaces;
using Lettre.Application.Searches;
using System.Collections.Generic;

namespace Lettre.Application.Commands.User
{
    public interface IGetUsersCommand : ICommand<UserSearch, IEnumerable<GetUserDto>>
    {
    }
}
