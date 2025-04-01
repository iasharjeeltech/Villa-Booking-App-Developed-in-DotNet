using System.Diagnostics;
using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Domain.Entities;
using eVillaBooking.Presentation.Models;
using eVillaBooking.Presentation.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace eVillaBooking.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                VillaList = _unitOfWork.VillaRepositoryUOW.GetAll(includeProperties:"AmenityList"),
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now)
            };
             
             return View(homeVM);
        }
        [HttpPost]
        public IActionResult GetVillaByDate(int nights, DateOnly checkInDate)
        {
            var villaList = _unitOfWork.VillaRepositoryUOW.GetAll(includeProperties: "AmenityList");

            foreach (Villa villa in villaList)
            {
                if(villa.Id % 2 == 0)
                {
                    villa.IsAvailable = false;
                }
            }

             var homeVM = new HomeVM()
            {
                VillaList = villaList,
                Nights = nights,
                CheckInDate = checkInDate
            };

            return PartialView("_VillaList",homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
