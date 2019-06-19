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
            catch (Exception e)
            {
                return StatusCode(500, "Serverska greška prilikom dohvatanja korisnika");
            }
        }

        // POST: api/Users
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
