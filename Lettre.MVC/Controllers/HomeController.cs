using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lettre.MVC.Models;
using Lettre.EfDataAccess;

namespace Lettre.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly LettreDbContext Context;

        public HomeController(LettreDbContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            var categories = Context.Categories;
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
