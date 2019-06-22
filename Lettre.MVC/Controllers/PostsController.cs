using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lettre.Application.Commands.Post;
using Lettre.Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lettre.MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly IGetPostsCommand _getPosts;
        private readonly IGetPostCommand _getPost;

        public PostsController(IGetPostsCommand getPosts, IGetPostCommand getPost)
        {
            _getPosts = getPosts;
            _getPost = getPost;
        }

        // GET: Posts
        public ActionResult Index(PostSearch search)
        {
            var psots = _getPosts.Execute(search);
            return View(psots);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int id)
        {
            var post = _getPost.Execute(id);
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Posts/Edit/5
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

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Posts/Delete/5
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