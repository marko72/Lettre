using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Post;
using Lettre.Application.DTO;
using Lettre.Application.DTO.Post;
using Lettre.Application.Searches;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.PostCommands
{
    public class EfGetPostsCommand : EfBaseCommand, IGetPostsCommand
    {
        public EfGetPostsCommand(LettreDbContext context) : base(context)
        {
        }

        public IEnumerable<GetPostsDto> Execute(PostSearch request)
        {
            var postsObj = Context.Posts.Include(p => p.Category).Include(p => p.Pictures).AsQueryable();
            if(request.CategoryId != 0)
            {
                postsObj = postsObj.Where(p => p.CategoryId == request.CategoryId);
            }
            if (!String.IsNullOrEmpty(request.Name))
            {
                postsObj = postsObj.Where(p => p.Title.ToLower().Contains(request.Name.ToLower()));
            }

            var posts = postsObj.Select(p => new GetPostsDto
            {
                Title = p.Title,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Content = p.Content,
                PicturesPath = p.Pictures.Select(pic => new GetPictureDto {
                    Path = pic.Path
                }).ToList()

            });
            return posts;
        }
    }
}
