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
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>All categories</returns>
        /// <response code="200">Uspesno dohvacene kategorije.</response>
        /// <response code="500">Serverska greska prilikom dohvatanja kategorija</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet]
        public ActionResult<IEnumerable<GetCategoryDto>> Get([FromQuery] CategorySearch search)
        {
            try
            {
                var categories = _getCategories.Execute(search);
                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greška prilikom dohvatanja kategorije");
            }
            

        }

        // GET: api/Category/5
        /// <summary>
        /// Get category by id
        /// </summary>
        /// <returns>One category</returns>
        /// <response code="200">Uspesno dohvacene kategorija.</response>
        /// <response code="404">Trazena kategorija ne postoji</response>
        /// <response code="500">Serverska greska prilikom dohvatanja kategorije</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<GetCategoryDto> Get(int id)
        {
            try
            {
                var category = _getCategory.Execute(id);
                return Ok(category);

            }catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greška prilikom dohvatanja kategorije");
            }

        }

        // POST: api/Category
        /// <summary>
        /// Insert new category
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="201">Uspesno kreirana kategorija.</response>
        /// <response code="409">Kategorija sa istim imenom vec postoji</response>
        /// <response code="500">Serverska greska prilikom unosa kategorija</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult Post([FromBody] CreateCategoryDto dto)
        {
            try {
                _createCategory.Execute(dto);
                return StatusCode(201, "Uspešno kreirana kategorija");
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
        /// <summary>
        /// Update category
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno izmenjena kategorija.</response>
        /// <response code="409">Kategorija sa istim imenom vec postoji</response>
        /// <response code="404">Kategorija koju zelite da izmenite ne postoji</response>
        /// <response code="500">Serverska greska prilikom izmene kategorije</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        [Produces("application/json")]
        public ActionResult Put(int id, [FromBody] GetCategoryDto dto)
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
        /// <summary>
        /// Delete category
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno obrisana kategorija.</response>
        /// <response code="404">Kategorija koju zelite da obrisete ne postoji</response>
        /// <response code="422">Prosledili ste nevažeću vrednost za brisanje kategorije</response>
        /// <response code="500">Serverska greska prilikom brisanja kategorija</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteCategory.Execute(id);
                return StatusCode(204,"Uspešno obrisana kategorija");
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
                return StatusCode(500);
            }

        }
    }
}
