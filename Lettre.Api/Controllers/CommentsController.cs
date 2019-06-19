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
        private readonly IEditCommentCommand _updateComment;
        private readonly IDeleteCommentCommand _deleteComment;

        public CommentsController(ICreateCommentCommand createComment, IEditCommentCommand updateComment, IDeleteCommentCommand deleteComment)
        {
            _createComment = createComment;
            _updateComment = updateComment;
            _deleteComment = deleteComment;
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
        public ActionResult Post([FromBody] CreateCommentDto dto)
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
                return StatusCode(500, "Serverska greška prilikom komentarisanja");
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditCommentDto dto)
        {
            try
            {
                _updateComment.Execute(dto);
                return StatusCode(200, "Uspešno izmenjen komentar");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidValueForwardedException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (EntityAlreadyExistException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greška prilikom izmene komentara");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteComment.Execute(id);
                return StatusCode(200, "Uspešno izmenjen komentar");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidValueForwardedException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greška prilikom brisanja komentara");
            }
        }
    }
}
