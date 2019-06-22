using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lettre.Application.Commands.User;
using Lettre.Application.DTO.User;
using Lettre.Application.Exceptions;
using Lettre.Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lettre.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICreateUserCommand _createUser;
        private readonly IGetUsersCommand _getUsers;
        private readonly IGetUserCommand _getUser;
        private readonly IUpdateUserCommand _updateUser;
        private readonly IDeleteUserCommand _deleteUser;

        public UsersController(ICreateUserCommand createUser, IGetUsersCommand getUsers, IGetUserCommand getUser, IUpdateUserCommand updateUser, IDeleteUserCommand deleteUser)
        {
            _createUser = createUser;
            _getUsers = getUsers;
            _getUser = getUser;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
        }


        // GET: api/Users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>All users</returns>
        /// <response code="200">Uspesno dohvaceni korisnici.</response>
        /// <response code="404">Nema ni jednog korisnika</response>
        /// <response code="500">Serverska greska prilikom dohvatanja korisnika</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet]
        public ActionResult<IEnumerable<GetUserDto>> Get([FromQuery]UserSearch search)
        {
            try
            {
                var users = _getUsers.Execute(search);
                return Ok(users);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greska prilikom dohvatanja korisnika");
            }
        }

        // GET: api/Users/5
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns>One user</returns>
        /// <response code="200">Uspesno dohvacen korisnik.</response>
        /// <response code="404">Trazeni korisnik ne postoji</response>
        /// <response code="500">Serverska greska prilikom dohvatanja korisnika</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpGet("{id}", Name ="GetUser")]
        public ActionResult<GetUserDto> Get(int id)
        {
            try
            {
                var user = _getUser.Execute(id);
                return Ok(user);
            }
            catch(EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greška prilikom dohvatanja korisnika");
            }
        }

        // POST: api/Users
        /// <summary>
        /// Insert new user
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="201">Uspesno kreiran korisnik.</response>
        /// <response code="409">Korisnik sa tim imejlom vec postoji</response>
        /// <response code="404">Uloga koju zelite da dodelite korisniku je obrisana ili ne postoji</response>
        /// <response code="500">Serverska greska prilikom unosa korsnika</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult Post([FromBody] CreateUserDto dto)
        {
            try
            {
                _createUser.Execute(dto);
                return StatusCode(201,"Uspešno kreiran korisnik");
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
                return StatusCode(500, "Serverska greška prilikom unosa korisnika");
            }
        }

        // PUT: api/Users/5
        /// <summary>
        /// Update user
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno izmenjen korisnik.</response>
        /// <response code="409">Korisnik sa istim imejlom vec postoji</response>
        /// <response code="404">Korisnik koga zelite da izmenite je obrisan ili ne postoji</response>
        /// <response code="404">Uloga koju zelite da dodelite korisniku ne postoji</response>
        /// <response code="500">Serverska greska prilikom izmene kategorije</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateUserDto dto)
        {
            try
            {
                _updateUser.Execute(dto);
                return StatusCode(204, "Uspesno izmenjen korisnik");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityAlreadyExistException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Serverska greška prilikom izmene korisnika");
            }
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Delete user
        /// </summary>
        /// <returns>Status code</returns>
        /// <response code="204">Uspesno obrisan korisnik.</response>
        /// <response code="404">Korisnik koga zelite da obrisete ili je vec obrisan ili ne postoji</response>
        /// <response code="500">Serverska greska prilikom brisanja kategorija</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteUser.Execute(id);
                return StatusCode(204,"Uspešno obrisan korisnik");
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Serverska greska prilikom brisanja korisnika");
            }
        }
    }
}
