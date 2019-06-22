using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Searches
{
    public class PostSearchApi : Pagination
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
