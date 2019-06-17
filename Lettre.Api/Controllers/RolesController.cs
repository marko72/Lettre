using System;
using Lettre.Application.Commands.Role;
using Lettre.Application.DTO.Role;
using Lettre.Application.Exceptions;
using Lettre.Application.Searches;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lettre.Api.Controllers
{
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly IGetRoleCommand _getRole;
        private readonly ICreateRoleCommand _createRole;
        private readonly IGetRolesCommand _getRoles;
        private readonly IUpdateRoleCommand _updateRole;
        private readonly IDeleteRoleCommand _deleteRole;

        public RolesController(IGetRoleCommand getRole, ICreateRoleCommand createRole, IGetRolesCommand getRoles, IUpdateRoleCommand updateRole, IDeleteRoleCommand deleteRole)
        {
            _getRole = getRole;
            _createRole = createRole;
            _getRoles = getRoles;
            _updateRole = updateRole;
            _deleteRole = deleteRole;
        }
        //Potrebno je dodati Unique da ne bi uloga mogla isto da se zove
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromQuery]RoleSearch search)
        {
            return Ok(_getRoles.Execute(search));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                
                return Ok(_getRole.Execute(id));
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greska pri dohvatanju uloge");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]CreateRoleDto dto)
        {
            try
            {
                _createRole.Execute(dto);
                return StatusCode(201, "Uspesno kreirana uloga");
            }catch(EntityAlreadyExistException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Doslo je do greske na serveru");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GetRoleDto dto)
        {
            try
            {
                _updateRole.Execute(dto);
                return StatusCode(204, "Uloga uspesnon izmenjena");
            }
            catch (EntityAlreadyExistException e)
            {
                return Conflict(e.Message);
            }
            catch (EntityNotFoundException e )
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greska pri izmeni uloge");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteRole.Execute(id);
                return StatusCode(204, "Uspesno obrisana uloga");
            }catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
