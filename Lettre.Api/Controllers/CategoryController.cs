using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lettre.Application.Commands.Category;
using Lettre.Application.DTO.Category;
using Lettre.Application.Exceptions;
using Lettre.Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lettre.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICreateCategoryCommand _createCategory;
        private readonly IGetCategoriesCommand _getCategories;
        private readonly IGetCategoryCommand _getCategory;
        private readonly IUpdateCategoryCommand _updateCategory;
        private readonly IDeleteCategoryCommand _deleteCategory;

        public CategoryController(ICreateCategoryCommand createCategory, IGetCategoriesCommand getCategories, IGetCategoryCommand getCategory, IUpdateCategoryCommand updateCategory, IDeleteCategoryCommand deleteCategory)
        {
            _createCategory = createCategory;
            _getCategories = getCategories;
            _getCategory = getCategory;
            _updateCategory = updateCategory;
            _deleteCategory = deleteCategory;
        }


        // GET: api/Category
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search)
        {
            return Ok(_getCategories.Execute(search));
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_getCategory.Execute(id));
            }catch(EntityNotFoundException e)
            {
                return NotFound(e);
            }
            
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto dto)
        {
            try {
                _createCategory.Execute(dto);
                return Ok();

            }
            catch (EntityAlreadyExistException e)
            {
                return Conflict(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public IActionResult Put(int id, [FromBody] GetCategoryDto dto)
        {
            try
            {
                _updateCategory.Execute(dto);
                return StatusCode(204, "Uspesno izmenjena kategorija");
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
                return StatusCode(500, "Serverska greska pri izmeni kategorije, pokusajte ponovo!");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteCategory.Execute(id);
                return StatusCode(204);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
