using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Post;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.PostCommands
{
    public class EfDeletePostCommand : EfBaseCommand, IDeletePostCommand
    {
        public EfDeletePostCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            if(id == 0 || id < 0)
            {
                throw new InvalidValueForwardedException("Prosledili ste nevažeću vrednost za brisanje");
            }
            var post = Context.Posts.Include(p => p.Comments).Where(p => p.Id == id).First();
            if(post == null || post.IsDeleted)
            {
                throw new EntityNotFoundException("Vest koju želite da obrišete je već obrisana ili");
            }

            post.IsDeleted = true;
            var comms = post.Comments;
            foreach(var c in comms)
            {
                c.IsDeleted = true;
            }

            Context.SaveChanges();
        }
    }
}
