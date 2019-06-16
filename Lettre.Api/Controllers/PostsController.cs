﻿using System;
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

        public PostsController(IGetPostsCommand getPosts, ICreatePostCommand createPost, IGetPostCommand getPost, IEditPostCommand editPost)
        {
            _getPosts = getPosts;
            _createPost = createPost;
            _getPost = getPost;
            _editPost = editPost;
        }



        // GET: api/Posts
        [HttpGet]
        public IActionResult Get([FromQuery]PostSearch dto)
        {
            try
            {
                var result = _getPosts.Execute(dto);
                return Ok(result);
            }catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/Posts/5
        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult Get(int id)
        {
            try
            {
                var post = _getPost.Execute(id);
                return Ok(post);
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
        public IActionResult Post([FromForm] ApiPostDto apiDto)
        {
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
        public IActionResult Put(int id, [FromBody] EditPostDto dto)
        {
            try
            {
                _editPost.Execute(dto);
                return StatusCode(204, "Uspesno izmenjena vest");
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
        public void Delete(int id)
        {
        }
    }
}