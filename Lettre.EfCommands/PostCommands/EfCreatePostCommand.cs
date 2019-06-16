using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Post;
using Lettre.Application.DTO.Post;
using Lettre.Application.Exceptions;
using Lettre.Domain;
using Lettre.EfDataAccess;

namespace Lettre.EfCommands.PostCommands
{
    public class EfCreatePostCommand : EfBaseCommand, ICreatePostCommand
    {
        public EfCreatePostCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(CreatePostDto request)
        {
            if (Context.Posts.Where(p => p.IsDeleted != true).Any(p => p.Title == request.Title))
            {
                throw new EntityAlreadyExistException("Vest sa tim naslovom");
            }
            var category = Context.Categories.Find(request.CategoryId);
            if (category == null || category.IsDeleted == true)
            {
                throw new EntityNotFoundException("Kategorija kojoj ste dodelili vest");
            }
            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                CategoryId = request.CategoryId
            };
            /*var post = Context.Posts.Add(new Post
            {
                
            });*/
            var picture = new Picture
            {
                Path = request.PicturePath
            };
            picture.Post = post;

            Context.Pictures.Add(picture);
            Context.SaveChanges();
            /*var IdPost = post.Entity.Id;
            Context.Pictures.Add(new Picture
            {
                Path = request.PicturePath,
                PostId = IdPost
            });
            Context.SaveChanges();*/
        }
    }
}
