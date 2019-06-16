using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Comments;
using Lettre.Application.DTO.Comment;
using Lettre.Application.Exceptions;
using Lettre.Domain;
using Lettre.EfDataAccess;

namespace Lettre.EfCommands.CommentsCommands
{
    public class EfCreateCommentCommand : EfBaseCommand, ICreateCommentCommand
    {
        public EfCreateCommentCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(CreateCommentDto request)
        {
            var user = Context.Users.Find(request.UserId);

            if (user == null || user.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik koji zeli da komentarise ");
            }

            var post = Context.Posts.Find(request.PostId);
            if(post == null || post.IsDeleted == true)
            {
                throw new EntityNotFoundException("Post koji zelite da komentarisete");
            }

            if(request.Content == "")
            {
                throw new Exception("Sadrzaj komentara je prazan");
            }

            Context.Comments.Add(new Comment
            {
                Content = request.Content,
                PostId = request.PostId,
                UserId = request.UserId
            });

            Context.SaveChanges();
        }
    }
}
