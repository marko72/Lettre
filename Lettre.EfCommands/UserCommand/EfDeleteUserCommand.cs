using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lettre.Application.Commands.User;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;

namespace Lettre.EfCommands.UserCommand
{
    public class EfDeleteUserCommand : EfBaseCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var user = Context.Users.Find(id);
            if(user == null || user.IsDeleted == true)
            {
                throw new EntityNotFoundException("Korisnik koga zelite da obrisete");
            }

            user.IsDeleted = true;
            user.Comments.Select(com => com.IsDeleted == true);
            Context.SaveChanges();

        }
    }
}
