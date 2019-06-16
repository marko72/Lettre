using Lettre.Application.Commands.Category;
using Lettre.Application.DTO.Category;
using Lettre.Application.Exceptions;
using Lettre.Application.Interfaces;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.CategoryCommands
{
    public class UpdateCategoryCommand : EfBaseCommand, IUpdateCategoryCommand
    {
        public UpdateCategoryCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(GetCategoryDto request)
        {
            var cat = Context.Categories.Find(request.Id);

            if (cat == null || cat.IsDeleted == true)
            {
                throw new EntityNotFoundException("Kategorija koju zelite da izmenite");
            }
            if (request.Name == cat.Name)
            {
                throw new EntityAlreadyExistException("Ta kategorija");
            }
            if (Context.Categories.Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistException("Kategorija sa tim imenom");
            }
            cat.Name = request.Name;
            cat.ModifiedAt = DateTime.Now;
            Context.SaveChanges();
        }
    }
}
