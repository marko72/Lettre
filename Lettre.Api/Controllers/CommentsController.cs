using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lettre.Application.Commands.Comments;
using Lettre.Application.DTO.Comment;
using Lettre.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lettre.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICreateCommentCommand _createComment;

        public CommentsController(ICreateCommentCommand createComment)
        {
            _createComment = createComment;
        }

        // GET: api/Comments
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Comments/5
        [HttpGet("{id}", Name = "GetComment")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Comments
        [HttpPost]
        public IActionResult Post([FromBody] CreateCommentDto dto)
        {
            try
            {
                _createComment.Execute(dto);
                return StatusCode(201, "Uspesno komentarisana vest");

            }
            catch (EntityNotFoundException e)
            {

                return NotFound(e.Message);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
