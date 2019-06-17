using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.User;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;

namespace Lettre.EfCommands.UserCommand
{
    public class EfDeleteUserCommand : EfBaseCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var user = Context.Users.Include(u => u.Comments).Where(u => u.Id == id).First();
            if(user == null || user.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik koga zelite da obrisete");
            }

            user.IsDeleted = true;
            var comms = user.Comments;
            foreach(var c in comms)
            {
                c.IsDeleted = true;
            }
            Context.SaveChanges();

        }
    }
}
