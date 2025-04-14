using eVillaBooking.Application.Common.Interfaces;
using eVillaBooking.Application.Utility;
using eVillaBooking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace eVillaBooking.Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

       [Authorize]
        public IActionResult FinalizeBooking(DateOnly checkInDate,int nights,int villaId)

        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser = _unitOfWork.ApplicationUserRepositoryUOW.Get(au => au.Id == userId);
            if (applicationUser == null)
            {
                // Handle the case where the user is not found
                return NotFound("User not found");
            }

            Villa villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == villaId, includeProperties: "AmenityList");
            if (villa == null)
            {
                // Handle the case where the villa is not found
                return NotFound("Villa not found");
            }

            Booking booking = new()
            {
                CheckInDate = checkInDate,
                Nights = nights,
                VillaId = villaId,
                CheckOutDate = checkInDate.AddDays(nights),
                Villa = villa,
                Name = applicationUser.Name,
                Email = applicationUser.Email,
                Phone = applicationUser.PhoneNumber,
                UserId = userId
            };

            booking.TotalCost = booking.Villa.Price * nights;
            return View(booking);

        }
        
        [Authorize, HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {
            // Get the villa details
            Villa villa = _unitOfWork.VillaRepositoryUOW.Get(v => v.Id == booking.VillaId);

            // Set booking details
            booking.TotalCost = villa.Price * booking.Nights;
            booking.BookingDate = DateTime.Now;
            booking.Status = StaticDetails.StatusApproved;

            // Save booking to the database
            _unitOfWork.BookingRepositoryUOW.Add(booking);
            _unitOfWork.Save();

            var domain = Request.Scheme + "://" + Request.Host.Value + "/";

            // Prepare Stripe payment session
            SessionCreateOptions options = new SessionCreateOptions
            {
              LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = (long)(booking.TotalCost * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = booking.Name,
                                Description = booking.Villa.Description
                            }

                        }
                    }
                },
              Mode = "payment",
              SuccessUrl = domain + $"Booking/BookingConfirmation?bookingId={booking.Id}",
              CancelUrl = domain + $"Booking/FinalizeBooking?checkInDate={booking.CheckInDate}&nights={booking.Nights}&villaId={booking.VillaId}"
            };
            
            //DateOnly checkInDate,int nights,int villaId

            var service = new SessionService();
            Session session = service.Create(options);

            _unitOfWork.BookingRepositoryUOW.UpdateStripePaymentId(booking.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();

            return Redirect(session.Url);
            // Redirect to confirmation page
            //return RedirectToAction(nameof(BookingConfirmation), new { bookingId = booking.Id });
        }

        public IActionResult BookingConfirmation(int bookingId)
        {
            var bookingFromDb = _unitOfWork.BookingRepositoryUOW.Get(b => b.Id == bookingId, includeProperties: "Villa,User");
            if(bookingFromDb.Status == StaticDetails.StatusPending)
            {
                SessionService sessionService = new SessionService();
                Session session = sessionService.Get(bookingFromDb.StripeSessionId);
                if(session.PaymentStatus == "paid")
                {
                    _unitOfWork.BookingRepositoryUOW.UpdateStatus(bookingId, StaticDetails.StatusPending);
                    _unitOfWork.BookingRepositoryUOW.UpdateStripePaymentId(bookingId, session.Id, session.PaymentIntentId);
                    _unitOfWork.Save();
                }
            }
            return View(bookingId);
        }

        public IActionResult Details(int id)
        {
            var bookingFromDb = _unitOfWork.BookingRepositoryUOW
                                .Get(b => b.Id == id, includeProperties: "Villa.AmenityList,User");


            return View(bookingFromDb);
        }

        #region APIs

        [Authorize, HttpGet]
        public IActionResult GetAllBookings()
        {
            IEnumerable<Booking> bookingObj= Enumerable.Empty<Booking>();
            if (User.IsInRole(StaticDetails.Role_Admin))
            {
                 bookingObj = _unitOfWork.BookingRepositoryUOW.GetAll(includeProperties:"User,Villa");
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                bookingObj = _unitOfWork.BookingRepositoryUOW.GetAll(b => b.UserId==userId, includeProperties: "User,Villa");

            }
            //yahan json data bhej rahe hai with key!
            return Json(new {data = bookingObj });
        }
        #endregion
    }
}
