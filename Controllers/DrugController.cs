using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DrugManufacturing.Models;
using DrugManufacturing.Entities;
using DrugManufacturing.Data;

namespace DrugManufacturing.Controllers
{
    public class DrugController : Controller
    {
        private readonly DrugManufacturingContext _context;

        public DrugController(DrugManufacturingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Personal", "Drug");
        }

        public IActionResult All()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null && user.Manufacturer != null)
                {
                    ViewBag.User = user;
                    List<Drug> drugs = _context.Drugs.Where(d => d.DrugManufacturers.Where(dm => dm.Manufacturer.Id == user.Manufacturer.Id).FirstOrDefault() == null).ToList();
                    ViewBag.Drugs = drugs;
                    return View();
                }
                return RedirectToAction("Index", "Drug");
            }
            catch { }
            return RedirectToAction("Login", "User");
        }

        public IActionResult Personal()
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
                    if (user.Applicant != null)
                    {
                        List<Drug> drugs = _context.Drugs.Where(d => d.Applicant.UserId == userId).ToList();
                        ViewBag.Drugs = drugs;

                    }
                    else if (user.Manufacturer != null)
                    {
                        List<Drug> drugs = _context.Drugs.Where(d => d.DrugManufacturers.Where(dm => dm.Manufacturer.Id == user.Manufacturer.Id).FirstOrDefault().DrugId == d.Id).ToList();
                        ViewBag.Drugs = drugs;
                    }
                    return View();
                }
            }
            catch { }
            return RedirectToAction("Login", "User");
        }

        public IActionResult Create()
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null && user.Applicant != null)
                {
                    ViewBag.User = user;
                    return View();
                }
            }
            catch { }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public IActionResult Create(DrugModel drugModel)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null && user.Applicant != null)
                {
                    if (ModelState.IsValid!)
                    {
                        Drug drug = new Drug
                        {
                            TradeName = drugModel.TradeName,
                            InternationalName = drugModel.InternationalName,
                            Form = drugModel.Form,
                            Formula = drugModel.Formula,
                            Applicant = user.Applicant
                        };
                        _context.Drugs.Add(drug);
                        _context.SaveChanges();
                        return RedirectToAction("Personal", "Drug");
                    }
                    return View();
                }
            }
            catch { }
            return RedirectToAction("Login", "User");
        }

        public IActionResult Update(int drugId)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null && user.Applicant != null)
                {
                    ViewBag.User = user;
                    Drug drug = _context.Drugs.Where(d => d.Id == drugId).Include(d => d.Applicant).FirstOrDefault();
                    if (drug == null || drug.Applicant.Id != user.Applicant.Id)
                    {
                        return RedirectToAction("Personal", "Drug");
                    }
                    return View(drug);
                }
            }
            catch { }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public IActionResult Update(DrugModel drugModel)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null && user.Applicant != null)
                {
                    if (ModelState.IsValid!)
                    {
                        Drug drug = _context.Drugs.Where(d => d.Id == drugModel.Id).Include(d => d.Applicant).FirstOrDefault();
                        if (drug != null || drug.Applicant.Id == user.Applicant.Id)
                        {
                            drug.TradeName = drugModel.TradeName;
                            drug.InternationalName = drugModel.InternationalName;
                            drug.Form = drugModel.Form;
                            drug.Formula = drugModel.Formula;
                            _context.SaveChanges();
                        }
                        return RedirectToAction("Personal", "Drug");
                    }
                }
            }
            catch { }
            return RedirectToAction("Login", "User");
        }

        public IActionResult AddToList(int drugId)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null && user.Manufacturer != null)
                {
                    ViewBag.User = user;
                    Drug drug = _context.Drugs.Where(d => d.Id == drugId).Include(d => d.DrugManufacturers).FirstOrDefault();
                    if (drug != null)
                    {
                        DrugManufacturer dm = new DrugManufacturer
                        {
                            DrugId = drug.Id,
                            Drug = drug,
                            ManufacturerId = user.Manufacturer.Id,
                            Manufacturer = user.Manufacturer
                        };
                        drug.DrugManufacturers.Add(dm);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("All", "Drug");
                }
            }
            catch { }
            return RedirectToAction("Login", "User");
        }

        public IActionResult Delete(int drugId)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("userId");
                User user = _context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Applicant)
                    .Include(u => u.Manufacturer)
                    .FirstOrDefault();

                if (user != null && user.Manufacturer != null)
                {
                    ViewBag.User = user;
                    Drug drug = _context.Drugs.Where(d => d.Id == drugId).Include(d => d.DrugManufacturers).FirstOrDefault();
                    if (drug != null && drug.DrugManufacturers != null)
                    {

                        List<DrugManufacturer> dms = new List<DrugManufacturer>();
                        foreach(var dm in drug.DrugManufacturers)
                        {
                            if (dm.ManufacturerId == user.Manufacturer.Id)
                            {
                                dms.Add(dm);
                            }
                        }
                        if (dms.Count > 0)
                        {
                            drug.DrugManufacturers.Remove(dms[0]);
                            _context.SaveChanges();
                        }
                    }
                    return RedirectToAction("Personal", "Drug");
                }
                else if (user != null && user.Applicant != null)
                {
                    ViewBag.User = user;
                    Drug drug = _context.Drugs.Where(d => d.Id == drugId).FirstOrDefault();
                    if (drug != null)
                    {
                        _context.Drugs.Remove(drug);
                        _context.SaveChanges();
                        return RedirectToAction("Personal", "Drug");
                    }
                    return View();
                }
                return RedirectToAction("Index", "Drug");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction("Login", "User");
        }
    }
}
