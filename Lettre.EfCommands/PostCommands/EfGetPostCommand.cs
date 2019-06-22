using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Post;
using Lettre.Application.DTO;
using Lettre.Application.DTO.Category;
using Lettre.Application.DTO.Comment;
using Lettre.Application.DTO.Post;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.PostCommands
{
    public class EfGetPostCommand : EfBaseCommand, IGetPostCommand
    {
        public EfGetPostCommand(LettreDbContext context) : base(context)
        {
        }

        public GetPostDto Execute(int id)
        {
            var post = Context.Posts
                .Include(p => p.Category)
                .Include(p => p.Pictures)
                .Include(p => p.Comments)
                .ThenInclude(com => com.User)
                .Where(p => p.Id==id)
                .First();

            if(post.IsDeleted == true || post == null)
            {
                throw new EntityNotFoundException("Vest koju želite da dohvatite");
            }
            

            return new GetPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Category = new GetCategoryWithoutPostsDto
                {
                    Name = post.Category.Name,
                    Id = post.Category.Id
                },
                Content = post.Content,
                PicturesPath = post.Pictures.Select(pic => new GetPictureDto
                {
                    Path = pic.Path
                }).ToList(),
                Comments = post.Comments.Where(c => c.IsDeleted == false).Select(com => new GetCommentsDto
                {

                    Content = com.Content,
                    CreatedAt = com.CreatedAt,
                    UserId = com.User.Id,
                    UserName = com.User.Name+" "+com.User.Surname
                }).ToList()
            };
        }
    }
}
