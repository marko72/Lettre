using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Searches
{
    public abstract class Pagination
    {
        public int PageNumber { get; set; } = 1;
        public int PerPage = 3;
    }
}
