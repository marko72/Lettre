using Lettre.Application.Commands.Post;
using Lettre.Application.DTO;
using Lettre.Application.DTO.Post;
using Lettre.Application.Responsed;
using Lettre.Application.Searches;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.PostCommands
{
    public class EfGetPaggedPostsCommand : EfBaseCommand, IGetPaggedPostsCommand
    {
        public EfGetPaggedPostsCommand(LettreDbContext context) : base(context)
        {
        }
        public PagedRespone<GetPostsDto> Execute(PostSearchApi request)
        {
            var postsObj = Context.Posts.Include(p => p.Category).Include(p => p.Pictures).AsQueryable();
            if (request.CategoryId != 0)
            {
                postsObj = postsObj.Where(p => p.CategoryId == request.CategoryId);
            }
            if (!String.IsNullOrEmpty(request.Name))
            {
                postsObj = postsObj.Where(p => p.Title.ToLower().Contains(request.Name.ToLower()));
            }
            postsObj = postsObj.Where(p => p.IsDeleted == false);
            var totalCount = postsObj.Count();

            postsObj = postsObj.Skip((request.PageNumber - 1) * request.PerPage)
                .Take(request.PerPage);

            var pageCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var posts = postsObj.Select(p => new GetPostsDto
            {
                Id = p.Id,
                Title = p.Title,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Content = p.Content,
                PicturesPath = p.Pictures.Select(pic => new GetPictureDto
                {
                    Path = pic.Path
                }).ToList()
            });
            var paggedPosts = new PagedRespone<GetPostsDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PageCount = pageCount,
                Data = posts
            };
            return paggedPosts;
        }
    }
}
