using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HSM.ViewModel
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "Customer name is required.")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer address is required.")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Booking From")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Booking from is required.")]

        public DateTime BookingFrom { get; set; }

        [Display(Name = "Booking To")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Booking to is required.")]

        public DateTime BookingTo { get; set; }

        [Display(Name = "Assign Room")]
        [Required(ErrorMessage = "Room is required.")]
        public int AssignRoomId { get; set; }

        [Display(Name = "Number of members")]
        [Required(ErrorMessage = "Number of members is required.")]
        public int NumberOfMembers { get; set; }
        public IEnumerable<SelectListItem> ListOfRooms { get; set; }

    }
}