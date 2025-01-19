using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructher.Data;
using eVillaBooking.Infrastructher.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IVillaRepository _villaRepository;
        public VillaController(IVillaRepository villaRepository)
        {
            _villaRepository = villaRepository;
        }

        public IActionResult Index()
        {
            //return View(_db.MyProperty.ToList());
            return View(_villaRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if(villa.Name == villa.Description)
            {
                ModelState.AddModelError("Name","Name & Description are same!");
            }
            if (ModelState.IsValid)
            {
                //_db.MyProperty.Add(villa);
                _villaRepository.Add(villa);
                //_db.SaveChanges();
                _villaRepository.Save();
                TempData["SuccessMessage"] = "Villa Added Successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(villa);
        }

        public IActionResult Edit(int id)
        {
            //var villa = _db.MyProperty.Find(id);
            var villa = _villaRepository.Get(v => v.Id == id);
            //var villa1 = _db.MyProperty.FirstOrDefault(v => v.Id == id);
            //var villa2 = _db.MyProperty.SingleOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Edit(Villa villa)
        {
            if (ModelState.IsValid) 
            {
                //_db.MyProperty.Update(villa);
                _villaRepository.Update(villa);
                _villaRepository.Save();
                //_db.SaveChanges();
                TempData["SuccessMessage"] = "Villa Updated Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Delete(int id)
        {
            //var villa = _db.MyProperty.Find(id);
            var villa = _villaRepository.Get(v => v.Id == id);
            if (villa is null)
            {
                return NotFound();
            }
            return View(villa);

        }

        //[HttpPost]
        //public IActionResult Delete (Villa villa)
        //{
        //    _db.MyProperty.Remove(villa);
        //    _db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
        //the above code is working properly 

        [HttpPost]
        public IActionResult DeleteConfirm (int id)
        {
            var villa = _villaRepository.Get(v => v.Id == id);
            //var villa = _db.MyProperty.Find(id);
            _villaRepository.Remove(villa);
            _villaRepository.Save();
             //_db.SaveChanges();
            TempData["SuccessMessage"] = "Villa Deleted Successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
