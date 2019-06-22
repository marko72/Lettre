using System;
using System.Collections;
using System.Collections.Generic;
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
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>All roles</returns>
        /// <response code="200">Uspesno dohvacene uloga.</response>
        /// <response code="500">Serverska greska prilikom dohvatanja uloga</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet]
        public ActionResult<IEnumerable<GetRoleDto>> Get([FromQuery]RoleSearch search)
        {
            
            try
            {
                var roles = _getRoles.Execute(search);
                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greska pri dohvatanju uloge");
            }
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get role by id
        /// </summary>
        /// <returns>One role</returns>
        /// <response code="200">Uspesno dohvacena uloga.</response>
        /// <response code="404">Trazena uloga ne postoji</response>
        /// <response code="500">Serverska greska prilikom dohvatanja uloge</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public ActionResult<GetRoleDto> Get(int id)
        {
            try
            {
                var role = _getRole.Execute(id);
                return Ok(role);
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
        /// <summary>
        /// Insert new role
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="201">Uspesno kreirana uloga.</response>
        /// <response code="409">Uloga sa tim nazivom vec postoji</response>
        /// <response code="500">Serverska greska prilikom unosa uloge</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult Post([FromBody]CreateRoleDto dto)
        {
            try
            {
                _createRole.Execute(dto);
                return StatusCode(201, "Uspesno kreirana uloga");
            }
            catch (EntityAlreadyExistException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Doslo je do greske na serveru");
            }
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update role
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno izmenjena uloga.</response>
        /// <response code="409">Uloga sa istim imenom vec postoji</response>
        /// <response code="404">Uloga koju zelite da izmenite ne postoji</response>
        /// <response code="500">Serverska greska prilikom izmene uloge</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] GetRoleDto dto)
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
        /// <summary>
        /// Delete role
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno obrisana uloga.</response>
        /// <response code="404">Uloga koju zelite da obrisete ne postoji</response>
        /// <response code="500">Serverska greska prilikom brisanja uloge</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
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
                return StatusCode(500, "Serverska greska pri brisanju uloge");
            }
        }
    }
}
