using Lettre.Application.DTO.Post;
using Lettre.Application.Interfaces;
using Lettre.Application.Responsed;
using Lettre.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Post
{
    public interface IGetPaggedPostsCommand :ICommand<PostSearchApi, PagedRespone<GetPostsDto>>
    {
    }
}
