using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eVillaBooking.Presentation.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;

        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villaNumber = _unitOfWork.VillaNumbersRepositoryUOW?.GetAll(includeProperties: "Villa");
            if (villaNumber == null)
            {
                return NotFound();
            }
            return View(villaNumber);
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
        public IActionResult Create(VillaNumber villaNumber)
        {
            bool isValidNumberExits = _unitOfWork.VillaNumbersRepositoryUOW.GetAll().Any(vn => vn.Villa_Number == villaNumber.Villa_Number);
            if (ModelState.IsValid && !isValidNumberExits)
            {
                //_villaNumberRepository.Add(villaNumber);
                _unitOfWork.VillaNumbersRepositoryUOW.Add(villaNumber);
                _unitOfWork.Save();
                //_db.VillaNumber.Add(villaNumber);
                //_villaNumberRepository.Save();
                //_db.SaveChanges();
                TempData["SuccessMessage"] = "Villa Number Added Successfully!";
                return RedirectToAction(nameof(Index));
            }
            //yeh niche dropdown ki list send ho rhi hai! 
            var selectListItem = _unitOfWork.VillaRepositoryUOW.GetAll().Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.Name
            }).ToList();

            ViewBag.SelectListItem = selectListItem;
            TempData["ErrorMessage"] = "Villa Number Already Exit!";
            return View(villaNumber);
        }

        public IActionResult Edit(int id)
        {
            var villaNumber = _unitOfWork.VillaNumbersRepositoryUOW.GetAll().FirstOrDefault(vn => vn.Villa_Number == id);

            if (villaNumber is null)
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

            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult Edit(VillaNumber villaNumber)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumbersRepositoryUOW.Update(villaNumber);
                _unitOfWork.Save();
                TempData["SuccessMessage"] = "Villa Number Updated Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(villaNumber);
        }

        public IActionResult Delete(int id)
        {
            //var villaNumber = _db.VillaNumber.FirstOrDefault(vn => vn.Villa_Number == id);
            var villaNumber = _unitOfWork.VillaNumbersRepositoryUOW.Get(vn => vn.Villa_Number == id);
            if (villaNumber is null)
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
            return View(villaNumber);
        }

        [HttpPost]
        //public IActionResult DeleteConfirm(IFormCollection formValues,int? villa_number)
        public IActionResult DeleteConfirm(int? villa_number)
        {
            //int test = Convert.ToInt16(formValues["Villa_Number"]);
            //var villaNumberToBeDeleted = _villaNumberRepository.GetAll().FirstOrDefault(vn => vn.Villa_Number == villa_number);
            var villaNumberToBeDeleted = _villaNumberRepository.Get(vn => vn.Villa_Number == villa_number);
            _unitOfWork.VillaNumbersRepositoryUOW.Remove(villaNumberToBeDeleted);
            _unitOfWork.Save();
            TempData["SuccessMessage"] = "Villa Number Deleted Successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
}
