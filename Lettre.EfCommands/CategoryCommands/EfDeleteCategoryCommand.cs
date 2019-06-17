using Lettre.Application.Commands.Category;
using Lettre.Application.Exceptions;
using Lettre.EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.CategoryCommands
{
    public class EfDeleteCategoryCommand : EfBaseCommand, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            if (id == 0 || id < 0)
            {
                throw new InvalidValueForwardedException("Prosledili ste nevažeću vrednost za brisanje kategorije");
            }
            var cat = Context.Categories
                .Include(c=> c.Posts)
                .ThenInclude(p => p.Comments)
                .SingleOrDefault(c => c.Id == id);
            if(cat == null || cat.IsDeleted == true)
            {
                throw new EntityNotFoundException("Kategorija koju zelite da obrisete");
            }
            cat.IsDeleted = true;
            var posts = cat.Posts;
            foreach(var p in posts)
            {
                if (p.IsDeleted == true)
                    continue;
                p.IsDeleted = true;
                var comms = p.Comments;
                foreach(var c in comms)
                {
                    if (c.IsDeleted == true)
                        continue;
                    c.IsDeleted = true;
                }
            }
            Context.SaveChanges();
        }
    }
}
