using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using HSM.Models;
using HSM.ViewModel;

namespace HSM.Controllers
{
    public class BookingController : Controller
    {
        private HotelEntities objHotelEntities;

        public BookingController()
        {
            objHotelEntities = new HotelEntities();
        }
        // GET: Room
        
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms = (from obj in objHotelEntities.Rooms
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomNumber,
                                                   Value = obj.RoomId.ToString()
                                               }).ToList();



            return View(objBookingViewModel);
        }
        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {


            RoomBooking roomBooking = new RoomBooking()
            {
                BookingId = objBookingViewModel.BookingId,
                CustomerName = objBookingViewModel.CustomerName,
                CustomerAddress = objBookingViewModel.CustomerAddress,
                BookingFrom = objBookingViewModel.BookingFrom,
                BookingTo = objBookingViewModel.BookingTo,
                AssignRoomId = objBookingViewModel.AssignRoomId,
                NoOfMembers = objBookingViewModel.NumberOfMembers



            };
            objHotelEntities.RoomBookings.Add(roomBooking);

            objHotelEntities.SaveChanges();
            return Json(data: new { message = "Booking Successfully", success = true }, JsonRequestBehavior.AllowGet);

        }
        



    }
}