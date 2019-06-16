using Lettre.Application.DTO.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lettre.Application.DTO.Category
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetPostsDto> PostsOfCategory { get; set; }
    }
}
