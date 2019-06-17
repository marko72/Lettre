using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Comments;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.CommentsCommands
{
    public class EfDeleteCommentCommand : EfBaseCommand,IDeleteCommentCommand
    {
        public EfDeleteCommentCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            if (id == 0 || id < 0)
            {
                throw new InvalidValueForwardedException("Prosledili ste nevažeću vrednost za brisanje komentara");
            }
            var comm = Context.Comments
                .Include(c => c.Post)
                .Include(c => c.User)
                .Where(c => c.Id == id)
                .First();
            if (comm == null || comm.IsDeleted == true)
            {
                throw new EntityNotFoundException("Komentar koji zelite da obrišete je obrisan ili");
            }
            if (comm.Post.IsDeleted == true || comm.Post == null)
            {
                throw new EntityNotFoundException("Vest čiji komentar želite da obrišete je obrisana ili");
            }
            if (comm.User == null || comm.User.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik koji želi da obriše komentar vest je banovan ili");
            }
            comm.IsDeleted = true;

            Context.SaveChanges();
        }
    }
}
