using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.Comments;
using Lettre.Application.DTO.Comment;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.CommentsCommands
{
    public class EfUpdateCommentCommand : EfBaseCommand, IEditCommentCommand
    {
        public EfUpdateCommentCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(EditCommentDto dto)
        {
            if(dto.Id == 0 || dto.Id < 0)
            {
                throw new InvalidValueForwardedException("Morate proslediti komentar koji želite da izmenite");
            }
            var comm = Context.Comments
                .Include(c => c.Post)
                .Include(c => c.User)
                .Where(c => c.Id == dto.Id)
                .First();
            if(comm == null || comm.IsDeleted == true)
            {
                throw new EntityNotFoundException("Komentar koji zelite da izmenite je obrisan ili");
            }
            if(comm.Post.IsDeleted == true || comm.Post == null)
            {
                throw new EntityNotFoundException("Vest čiji komentar želite izmeniti je obrisana ili");
            }
            if(comm.User == null || comm.User.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik koji želi da komentariše vest je banovan ili");
            }
            if(String.IsNullOrEmpty(dto.Content))
            {
                throw new InvalidValueForwardedException("Komentar ne može biti prazan");
            }
            if(comm.Content == dto.Content)
            {
                throw new EntityAlreadyExistException("Sadržaj komentara nije promenjen, stoga se ne može izmeniti");
            }
            comm.Content = dto.Content;

            Context.SaveChanges();
            
        }
    }
}
