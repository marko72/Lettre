using Lettre.Application.DTO.Category;
using Lettre.Application.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.DTO.Post
{
    public class GetPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public GetCategoryWithoutPostsDto Category { get; set; }
        public List<GetPictureDto> PicturesPath { get; set; }
        public List<GetCommentsDto> Comments { get; set; }
    }
}
