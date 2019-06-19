using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lettre.Api.Models;
using Lettre.Application.Commands.Post;
using Lettre.Application.DTO.Post;
using Lettre.Application.Exceptions;
using Lettre.Application.Helpers;
using Lettre.Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lettre.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IGetPostsCommand _getPosts;
        private readonly ICreatePostCommand _createPost;
        private readonly IGetPostCommand _getPost;
        private readonly IEditPostCommand _editPost;
        private readonly IDeletePostCommand _deletePost;

        public PostsController(IGetPostsCommand getPosts, ICreatePostCommand createPost, IGetPostCommand getPost, IEditPostCommand editPost, IDeletePostCommand deletePost)
        {
            _getPosts = getPosts;
            _createPost = createPost;
            _getPost = getPost;
            _editPost = editPost;
            _deletePost = deletePost;
        }



        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<GetPostsDto>> Get([FromQuery]PostSearch dto)
        {
            try
            {
                var result = _getPosts.Execute(dto);
                if(result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greška prilikom dohvatanja vesti");
            }
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        public ActionResult<GetPostDto> Get(int id)
        {
            try
            {
                var post = _getPost.Execute(id);
                return Ok(post);
            }
            catch (EntityAlreadyExistException e)
            {

                return Conflict(e.Message);
            }
            catch (InvalidValueForwardedException e)
            {

                return UnprocessableEntity(e.Message);
            }
            catch (EntityNotFoundException e)
            {

                return NotFound(e.Message);
            }
            catch (Exception)
            {

                return StatusCode(500, "Serverska greška prilikom dohvatanja vesti");
            }
        }

        // POST: api/Posts
        [HttpPost]
        public ActionResult Post([FromForm] ApiPostDto apiDto)
        {
            if(apiDto.Picture == null)
            {
                return UnprocessableEntity("Vest mora imati sliku");
            }
            try
            {
                var ext = Path.GetExtension(apiDto.Picture.FileName); 

                if (!FileUpload.ValidExtensions.Contains(ext))
                {
                    return UnprocessableEntity("Format slike nije dozvoljen.");
                }
                try
                {
                    var newFileName = Guid.NewGuid().ToString() + "_" + apiDto.Picture.FileName;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads","posts", newFileName);

                    apiDto.Picture.CopyTo(new FileStream(filePath, FileMode.Create));

                    var dto = new CreatePostDto
                    {
                        Title = apiDto.Title,
                        Content = apiDto.Content,
                        CategoryId = apiDto.CategoryId,
                        PicturePath = newFileName
                    };
                    _createPost.Execute(dto);
                    return StatusCode(201, "Uspesno kreirana vest");
                }
                catch (EntityAlreadyExistException e)
                {
                    return Conflict(e.Message);
                }
                catch (EntityNotFoundException e)
                {
                    return NotFound(e.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Serverska greska prilikom dodovanja vesti");
                }
            }
                catch (Exception)
                {
                    return StatusCode(500);
                }
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EditPostDto dto)
        {
            try
            {
                _editPost.Execute(dto);
                return StatusCode(200, "Uspesno izmenjena vest");
            }
            catch (EntityAlreadyExistException e)
            {
                return Conflict(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message/*"Serverska greška pri izmeni"*/);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deletePost.Execute(id);
                return StatusCode(200, "Uspešno obrisana vest");
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
                return StatusCode(500, "Serverska greška prilikom brisanja vesti");
            }

        }
    }
}
