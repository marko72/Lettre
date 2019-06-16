using Lettre.Application.DTO.Category;
using Lettre.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Commands.Category
{
    public interface IGetCategoryCommand : ICommand<int, GetCategoryDto>
    {
    }
}
