
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DrugManufacturing.Models;
using DrugManufacturing.Entities;
using DrugManufacturing.Data;
using DrugManufacturing.Services;
using System;

namespace DrugManufacturing.Controllers
{
    public class UserController : Controller
    {
        private readonly DrugManufacturingContext _context;

        public UserController(DrugManufacturingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

                if (user == null)
                {
                    return View();
                }
            } catch {}
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(Registration registration)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _context.Users.Where(u => u.Email == registration.Email).FirstOrDefault();
                if (existingUser == null)
                {
                    if (registration.Password1 == registration.Password2)
                    {
                        User user = new User
                        {
                            Email = registration.Email,
                            Password = PasswordHandler.CreatePasswordHash(registration.Password1)
                        };

                        _context.Users.Add(user);
                        _context.SaveChanges();

                        if (registration.RegistrationType == "Applicant")
                        {
                            Applicant applicant = new Applicant
                            {
                                Name = registration.Name,
                                Country = registration.Country,
                                City = registration.City,
                                StreetAddress = registration.StreetAddress,
                                UserId = user.Id
                            };

                            _context.Applicants.Add(applicant);
                            _context.SaveChanges();

                        }
                        else if (registration.RegistrationType == "Manufacturer")
                        {
                            Manufacturer manufacturer = new Manufacturer
                            {
                                Name = registration.Name,
                                Country = registration.Country,
                                City = registration.City,
                                StreetAddress = registration.StreetAddress,
                                UserId = user.Id
                            };

                            _context.Manufacturers.Add(manufacturer);
                            _context.SaveChanges();
                        }

                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Passwords aren't the same");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is already taken");

                }

            }
            return View(registration);
        }

        public IActionResult Login()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();

                if (user == null)
                {
                    return View();
                }
            }
            catch {}
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            User user = _context.Users.Where(u => u.Email == login.Email).FirstOrDefault();
            if (user != null)
            {
                if (PasswordHandler.Validate(login.Password, user.Password))
                {
                    HttpContext.Session.SetInt32("userId", user.Id);
                    return RedirectToAction("Index", "Home");
                }
            }
         
            ModelState.AddModelError(string.Empty, "Email or Password is incorrect.");
            return View(login);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            return RedirectToAction("Login", "User");
           
        }
    }
}
