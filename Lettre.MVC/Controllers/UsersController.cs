using System;
using System.Linq;
using Lettre.Application.Commands.User;
using Lettre.Application.DTO.Role;
using Lettre.Application.DTO.User;
using Lettre.Application.Exceptions;
using Lettre.Application.Interfaces;
using Lettre.Application.Searches;
using Lettre.Application.Responsed;
using Lettre.EfDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Lettre.MVC.Controllers
{
    public class UsersController : Controller
    {        
        private readonly ICreateUserCommand _createUser;
        private readonly IGetUsersCommand _getUsers;
        private readonly IGetUserCommand _getUser;
        private readonly IUpdateUserCommand _updateUser;
        private readonly IDeleteUserCommand _deleteUser;
        private readonly IEmailSender sender;
        private readonly LettreDbContext context;

        public UsersController(ICreateUserCommand createUser, IGetUsersCommand getUsers, IGetUserCommand getUser, IUpdateUserCommand updateUser, IDeleteUserCommand deleteUser, IEmailSender sender, LettreDbContext context)
        {
            _createUser = createUser;
            _getUsers = getUsers;
            _getUser = getUser;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
            this.sender = sender;
            this.context = context;
        }



        // GET: Users
        public ActionResult Index(UserSearch search)
        {
            try
            {
                var korisnici = _getUsers.Execute(search);
                return View(korisnici);
            }
            catch (EntityNotFoundException e)
            {
                TempData["greska"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["greska"] = "Serverska greska prilikom dohvatanja korisnika";
                return View();
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var user = _getUser.Execute(id);
                return View(user);
            }
            catch (EntityNotFoundException e)
            {
                TempData["greska"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["greska"] = "Serverska greška prilikom dohvatanja korisnika";
                return View();
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Roles = (context.Roles.Select(r => new GetRoleDto
            {
                Id = r.Id,
                Name = r.Name
            }));
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["greska"] = "Nisu dobri podaci";
                RedirectToAction("create");
            }
            try
            {

                _createUser.Execute(dto);
                sender.Subject = "Uspešno registrovanje!";
                sender.ToEmail = dto.Email;
                sender.Body = "Uspešno ste se registrovali na vaš sajt! Hvala vam što ste izabrali baš naše online novine!";
                sender.Send();
                TempData["success"] = "Uspešna registracija";
                return RedirectToAction(nameof(Index));
            }
            catch(EntityAlreadyExistException e)
            {
                TempData["greska"] = e.Message;
            }
            catch (EntityNotFoundException e)
            {
                TempData["greska"] = e.Message;
            }
            catch(Exception e)
            {
                TempData["greska"] = "Serverska greska prilikom unosa";
            }
            ViewBag.Roles = (context.Roles.Select(r => new GetRoleDto
            {
                Id = r.Id,
                Name = r.Name
            }));
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var user = _getUser.Execute(id);
                ViewBag.Roles = (context.Roles.Select(r => new GetRoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                }));
                return View(user);
            }
            catch (EntityNotFoundException e)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["greska"] = "Serverska greška prilikom dohvatanja korisnika";
                return View();
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateUserDto dto)
        {
            try
            {
                _updateUser.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException e)
            {
                ViewBag.Roles = (context.Roles.Select(r => new GetRoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                }));
                TempData["greska"] = e.Message;
                return View();
            }
            catch (EntityAlreadyExistException e)
            {
                ViewBag.Roles = (context.Roles.Select(r => new GetRoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                }));
                TempData["greska"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                ViewBag.Roles = (context.Roles.Select(r => new GetRoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                }));
                TempData["greska"] = "Serverska greška prilikom izmene korisnika";
                return View();
            }
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteUser.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException e)
            {
                TempData["greska"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["greska"] = "Serverska greska prilikom brisanja korisnika";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}