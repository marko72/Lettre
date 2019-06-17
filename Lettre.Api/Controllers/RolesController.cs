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
        [HttpGet]
        public ActionResult<IEnumerable<GetRoleDto>> Get([FromQuery]RoleSearch search)
        {
            
            try
            {
                var roles = _getRoles.Execute(search);
                if(roles == null)
                {
                    return NoContent();
                }
                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greska pri dohvatanju uloge");
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<GetRoleDto> Get(int id)
        {
            try
            {
                var role = _getRole.Execute(id);
                if(role == null)
                {
                    return NoContent();
                }
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
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] GetRoleDto dto)
        {
            try
            {
                _updateRole.Execute(dto);
                return StatusCode(200, "Uloga uspesnon izmenjena");
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
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteRole.Execute(id);
                return StatusCode(200, "Uspesno obrisana uloga");
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
