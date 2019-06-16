using Lettre.Application.Commands.Category;
using Lettre.Application.DTO.Category;
using Lettre.Application.Interfaces;
using Lettre.Application.Searches;
using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lettre.EfCommands.CategoryCommands
{
    public class EfGetCategoriesCommand : EfBaseCommand, IGetCategoriesCommand
    {
        public EfGetCategoriesCommand(LettreDbContext context) : base(context)
        {
        }

        public IEnumerable<GetCategoryDto> Execute(CategorySearch request)
        {
            var query = Context.Categories.AsQueryable();
            if (request.Name != null)
            {
                query = Context.Categories.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));
            }
            if (request.IsActive != null)
            {
                query = Context.Categories.Where(c => c.IsDeleted == request.IsActive);
            }

            return query.Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
