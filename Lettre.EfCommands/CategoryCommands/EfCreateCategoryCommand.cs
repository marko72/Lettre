using Lettre.Application.Commands.Category;
using Lettre.Application.DTO.Category;
using Lettre.Application.Exceptions;
using Lettre.Domain;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.CategoryCommands
{
    public class EfCreateCategoryCommand : EfBaseCommand, ICreateCategoryCommand
    {
        public EfCreateCategoryCommand(LettreDbContext context) : base(context)
        {
        }

        public void Execute(CreateCategoryDto request) 
        {
            if (Context.Categories.Any(c => c.Name == request.Name))
            {
                throw new EntityAlreadyExistException("Category");
            }
            Context.Categories.Add(new Category
            {
                Name = request.Name
            });
            Context.SaveChanges();
        }
    }
}
