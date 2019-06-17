using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Post;
using Lettre.Application.DTO.Post;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.PostCommands
{
    public class EfEditPostCommand : EfBaseCommand, IEditPostCommand
    {
        public EfEditPostCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(EditPostDto request)
        {
            if (request.Id == 0 || request.Id < 0)
            {
                throw new InvalidValueForwardedException("Morate proslediti vest koju želite da izmenite");
            }
            var post = Context.Posts.Find(request.Id);
            if(post == null || post.IsDeleted == true)
            {
                throw new EntityNotFoundException("Ne postoji vest za izmenu");
            }
            if (!String.IsNullOrEmpty(request.Title))
                if (Context.Posts.Any(p => p.Title == request.Title))
                {
                    throw new EntityAlreadyExistException("Vest sa identičnim naslovom");
                }
                else
                {
                    post.Title = request.Title;
                }
            if (!String.IsNullOrEmpty(request.Content))
                post.Content = request.Content;
            if(request.CategoryId != 0)
            {
                if(request.CategoryId < 0)
                {
                    throw new InvalidValueForwardedException("Pogrešna vrednost vesti je uneta");
                }
                if (!Context.Categories.Any(c => c.Id == request.CategoryId))
                {
                    throw new EntityNotFoundException("kategorija kojoj želite dodeliti vest");
                }
               post.CategoryId = request.CategoryId;
            }
            
            post.ModifiedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
