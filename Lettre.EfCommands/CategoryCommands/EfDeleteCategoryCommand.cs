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
            var cat = Context.Categories
                .Include(c=> c.Posts)
                .ThenInclude(p => p.Comments)
                .SingleOrDefault(c => c.Id == id);
            if(cat == null || cat.IsDeleted == true)
            {
                throw new EntityNotFoundException("Kategorija koju zelite da obrisete");
            }
            cat.IsDeleted = true;
            cat.Posts.Select(post => post.IsDeleted = true);
            cat.Posts.Select(post => post.Comments.Select(com => com.IsDeleted = true));
            Context.SaveChanges();
        }
    }
}
