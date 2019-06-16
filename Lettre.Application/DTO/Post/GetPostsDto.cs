using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.DTO.Post
{
    public class GetPostsDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public List<GetPictureDto> PicturesPath { get; set; }
        public string CategoryName { get; set; }
    }
}
