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
        /// <summary>
        /// Insert new comment
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="201">Uspesno komentarisana vest.</response>
        /// <response code="404">Vest koju zelite da komentarisete je obrisana ili ne postoji</response>
        /// <response code="500">Serverska greska prilikom unosa kategorija</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
        /// <summary>
        /// Update comment
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno izmenjen komentar.</response>
        /// <response code="404">Komentar koji zelite da izmenite je obrisan ili ne postoji</response>
        /// <response code="500">Serverska greska prilikom izmene komentara</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditCommentDto dto)
        {
            try
            {
                _updateComment.Execute(dto);
                return StatusCode(204, "Uspešno izmenjen komentar");
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
        /// <summary>
        /// Delete comment
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno obrisan komentar.</response>
        /// <response code="404">Komentar koji zelite da obrisete ne postoji</response>
        /// <response code="422">Prosledili ste nevažeću vrednost za brisanje komentara</response>
        /// <response code="500">Serverska greska prilikom brisanja komentara</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteComment.Execute(id);
                return StatusCode(204, "Uspešno obrisan komentar");
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
