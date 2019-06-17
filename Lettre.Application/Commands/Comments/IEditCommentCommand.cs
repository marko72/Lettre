using Lettre.Application.DTO.Comment;
using Lettre.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Comments
{
    public interface IEditCommentCommand : ICommand<EditCommentDto>
    {
    }
}
