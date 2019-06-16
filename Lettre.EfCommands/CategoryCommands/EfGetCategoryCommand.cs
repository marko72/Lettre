using Lettre.Application.Commands.Category;
using Lettre.Application.DTO;
using Lettre.Application.DTO.Category;
using Lettre.Application.DTO.Post;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.CategoryCommands
{
    public class EfGetCategoryCommand : EfBaseCommand, IGetCategoryCommand
    {
        public EfGetCategoryCommand(LettreDbContext context) : base(context)
        {
        }

        public GetCategoryDto Execute(int
            request)
        {
            var data = Context.Categories
                .Include(c => c.Posts)
                .ThenInclude(p => p.Pictures)
                .SingleOrDefault(c => c.Id == request);  
            if (data == null || data.IsDeleted == true)
            {
                throw new EntityNotFoundException("Kategorija");
            }
            return new GetCategoryDto
            {
                Id = data.Id,
                Name = data.Name,
                PostsOfCategory = data.Posts.Select(p => new GetPostsDto
                {
                    Content = p.Content,
                    Title = p.Title,
                    PicturesPath = p.Pictures.Select(pic => new GetPictureDto
                    {
                        Path = pic.Path
                    }).ToList()
                }).ToList()
            };

        }
    }
}
