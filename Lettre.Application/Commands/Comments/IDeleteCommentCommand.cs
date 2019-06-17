using Lettre.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Comments
{
    public interface IDeleteCommentCommand : ICommand<int>
    {
    }
}
