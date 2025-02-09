using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Infrastructher.Data;
using eVillaBooking.Infrastructher.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepository _villaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //return View(_db.MyProperty.ToList());
            return View(_unitOfWork.VillaRepositoryUOW.GetAll());
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
                if(villa.Image is not null)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    
                    string imagePath = Path.Combine(webRootPath, @"Images\VillaImages");

                    string newFileName = "MyImage" + "_" + Guid.NewGuid().ToString().Substring(0,5)+Path.GetExtension(villa.Image.FileName);
                    
                    string finalImagePath = Path.Combine(imagePath, newFileName);

                    using(FileStream fileStream = new FileStream(finalImagePath, FileMode.Create))
                    {
                        villa.Image.CopyTo(fileStream);
                        villa.ImageUrl = Path.Combine(@"\Images\VillaImages", newFileName);
                    } 
                }
                else
                {
                    villa.ImageUrl = "www.w  ebbears.in"; 
                }
                //_db.MyProperty.Add(villa);
                _unitOfWork.VillaRepositoryUOW.Add(villa);
                //_db.SaveChanges();
                _unitOfWork.Save();
                TempData["SuccessMessage"] = "Villa Added Successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(villa);
        }

        public IActionResult Edit(int id)
        {
            //var villa = _db.MyProperty.Find(id);
            var villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == id);
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
                if (villa.Image is not null)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;

                    string imagePath = Path.Combine(webRootPath, @"Images\VillaImages");

                    string newFileName = "MyImage" + "_" + Guid.NewGuid().ToString().Substring(0, 5) + Path.GetExtension(villa.Image.FileName);

                    string finalImagePath = Path.Combine(imagePath, newFileName);
                    if (!string.IsNullOrEmpty(villa.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(webRootPath, villa.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (FileStream fileStream = new FileStream(finalImagePath, FileMode.Create))
                    {
                        villa.Image.CopyTo(fileStream);
                        villa.ImageUrl = Path.Combine(@"\Images\VillaImages", newFileName);
                    }
                } 

                //_db.MyProperty.Update(villa);
                _unitOfWork.VillaRepositoryUOW.Update(villa);
                _unitOfWork.Save();
                //_db.SaveChanges();
                TempData["SuccessMessage"] = "Villa Updated Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(villa);
        }

        public IActionResult Delete(int id)
        {
            //var villa = _db.MyProperty.Find(id);
            var villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == id);
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
            var villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == id);

            if (!string.IsNullOrEmpty(villa.ImageUrl))
            {
                string webRootPath = _webHostEnvironment.WebRootPath;

                string oldImagePath = Path.Combine(webRootPath, villa.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            //var villa = _db.MyProperty.Find(id);
            _unitOfWork.VillaRepositoryUOW.Remove(villa);
            _unitOfWork.Save();
             //_db.SaveChanges();
            TempData["SuccessMessage"] = "Villa Deleted Successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
