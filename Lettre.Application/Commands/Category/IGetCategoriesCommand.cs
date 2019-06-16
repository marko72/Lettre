using Lettre.Application.DTO.Category;
using Lettre.Application.Interfaces;
using Lettre.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Category
{
    public interface IGetCategoriesCommand : ICommand<CategorySearch, IEnumerable<GetCategoryDto>>
    {
    }
}
