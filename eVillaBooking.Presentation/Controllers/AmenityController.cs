using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVillaBooking.Presentation.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;

        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenities = _unitOfWork.AmenityRepositoryUOW?.GetAll(includeProperties: "Villa");
            if (amenities == null)
            {
                return NotFound();
            }
            return View(amenities);
        }

        public IActionResult Create()
        {
            IEnumerable<Villa> MyProperty = _unitOfWork.VillaRepositoryUOW.GetAll();

            IEnumerable<SelectListItem> selectListItems = MyProperty.Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });

            ViewData["SelectListItem"] = selectListItems;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Amenity amenity)
        {
            //bool isValidNumberExits = _unitOfWork.VillaRepositoryUOW.GetAll().Any(vn => vn.Villa_Number == villaNumber.Villa_Number);
            if (ModelState.IsValid)
            {
                //_villaNumberRepository.Add(villaNumber);
                _unitOfWork.AmenityRepositoryUOW.Add(amenity);
                _unitOfWork.Save();
                //_db.VillaNumber.Add(villaNumber);
                //_villaNumberRepository.Save();
                //_db.SaveChanges();
                TempData["SuccessMessage"] = "Amenity Added Successfully!";
                return RedirectToAction(nameof(Index));
            }
            //yeh niche dropdown ki list send ho rhi hai! 
            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();

            ViewBag.SelectListItem = selectListItem;
            TempData["ErrorMessage"] = "Amenity Already Exit!";
            return View(amenity);
        }

        public IActionResult Edit(int id)
        {
            var amenity = _unitOfWork.AmenityRepositoryUOW.GetAll().FirstOrDefault(vn => vn.Id == id);

            if (amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }

            //yeh niche dropdown ki list send ho rhi hai! 
            var selectListItem = _villaRepository.GetAll().Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;

            return View(amenity);
        }

        [HttpPost]
        public IActionResult Edit(Amenity amenity)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.AmenityRepositoryUOW.Update(amenity);
                _unitOfWork.Save();
                TempData["SuccessMessage"] = "Amenity Updated Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(amenity);
        }

        public IActionResult Delete(int id)
        {
            //var villaNumber = _db.VillaNumber.FirstOrDefault(vn => vn.Villa_Number == id);
            var amenity = _unitOfWork.AmenityRepositoryUOW.Get(vn => vn.Id == id);
            if (amenity is null)
            {
                return NotFound();
            }

            //yeh niche dropdown ki list send ho rhi hai! 
            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();

            ViewData["SelectListItem"] = selectListItem;
            return View(amenity);
        }

        [HttpPost]
        //public IActionResult DeleteConfirm(IFormCollection formValues,int? villa_number)
        public IActionResult DeleteConfirm(int? id)
        {

            //int test = Convert.ToInt16(formValues["Villa_Number"]);
            var amenity = _unitOfWork.AmenityRepositoryUOW.Get(vn => vn.Id == id);
            //var amenity = _villaNumberRepository.Get(vn => vn.Villa_Number == villa_number);
            _unitOfWork.AmenityRepositoryUOW.Remove(amenity);
            _unitOfWork.Save();
            TempData["SuccessMessage"] = "Amenity Deleted Successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
