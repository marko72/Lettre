using Lettre.Application.DTO.User;
using Lettre.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.User
{
    public interface IUpdateUserCommand : ICommand<UpdateUserDto>
    {
    }
}
