using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lettre.Application.Commands.User;
using Lettre.Application.DTO.Role;
using Lettre.Application.DTO.User;
using Lettre.Application.Exceptions;
using Lettre.Application.Searches;
using Lettre.EfDataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lettre.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly LettreDbContext Context;
        private readonly ICreateUserCommand _createUser;
        private readonly IGetUsersCommand _getUsers;

        public UsersController(LettreDbContext context, ICreateUserCommand createUser, IGetUsersCommand getUsers)
        {
            Context = context;
            _createUser = createUser;
            _getUsers = getUsers;
        }

        // GET: Users
        public ActionResult Index(UserSearch search)
        {
            var korisnici = _getUsers.Execute(search);
            return View(korisnici);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Roles = Context.Roles.Select(r => new GetRoleDto
            {
                Id = r.Id,
                Name = r.Name
            });
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
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}