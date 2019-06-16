using Lettre.Application.DTO.Post;
using Lettre.Application.Interfaces;
using Lettre.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Post
{
    public interface IGetPostsCommand : ICommand<PostSearch, IEnumerable<GetPostsDto>>
    {
    }
}
