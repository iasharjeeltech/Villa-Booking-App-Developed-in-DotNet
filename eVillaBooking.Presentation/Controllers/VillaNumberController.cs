﻿using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructher.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var villaNumber = _db.VillaNumber.ToList();
            var villaNumber = _db.VillaNumber.Include(vn => vn.Villa).ToList();
            return View(villaNumber);
        }

        public IActionResult Create()
        {
            List<Villa> MyProperty = _db.MyProperty.ToList();
            
            IEnumerable<SelectListItem> selectListItems = MyProperty.Select(v => new SelectListItem
            {
                Text= v.Name,
                Value = v.Id.ToString()
            });

            ViewData["SelectListItem"]= selectListItems;
            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaNumber villaNumber)
        {
            bool isValidNumberExits = _db.VillaNumber.Any(vn => vn.Villa_Number == villaNumber.Villa_Number);
            if (ModelState.IsValid && !isValidNumberExits)
            {
                _db.VillaNumber.Add(villaNumber);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Villa Number Added Successfully!";
                return RedirectToAction(nameof(Index));
            }
            //yeh niche dropdown ki list send ho rhi hai! 
            var selectListItem = _db.MyProperty.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();

            ViewData ["SelectListItem"]= selectListItem;
            TempData["ErrorMessage"] = "Villa Number Already Exit!";
            return View(villaNumber);
        }

        public IActionResult Edit(int id)
        {
            var villaNumber = _db.VillaNumber.FirstOrDefault(vn => vn.Villa_Number==id);
            
            if (villaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }

            //yeh niche dropdown ki list send ho rhi hai! 
            var selectListItem = _db.MyProperty.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;

            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult Edit(VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
                _db.VillaNumber.Update(villaNumber);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Villa Number Updated Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(villaNumber);
        }

        public IActionResult Delete(int id)
        {
            var villaNumber = _db.VillaNumber.FirstOrDefault(vn => vn.Villa_Number==id);
            if (villaNumber is null)
            {
                return NotFound();
            }

            //yeh niche dropdown ki list send ho rhi hai! 
            var selectListItem = _db.MyProperty.Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;
            return View(villaNumber);
        }

        [HttpPost]
        //public IActionResult DeleteConfirm(IFormCollection formValues,int? villa_number)
        public IActionResult DeleteConfirm( int? villa_number)
        {
            //int test = Convert.ToInt16(formValues["Villa_Number"]);
            var villaNumberToBeDeleted = _db.VillaNumber.FirstOrDefault(vn => vn.Villa_Number==villa_number);
            _db.VillaNumber.Remove(villaNumberToBeDeleted);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Villa Number Deleted Successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
