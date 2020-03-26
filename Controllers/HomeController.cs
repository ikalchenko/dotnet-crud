using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DrugManufacturing.Models;
using DrugManufacturing.Entities;
using DrugManufacturing.Data;

namespace DrugManufacturing.Controllers
{
    public class HomeController : Controller
    {
        private readonly DrugManufacturingContext _context;

        public HomeController(DrugManufacturingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null)
                {
                    ViewBag.User = user;
                    return View();
                }
            }
            catch {}
            return RedirectToAction("Login", "User");
        }
   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
