using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShowtimeTestingProject.Controllers;
using ShowtimeWebApplication.Models;
using System;

namespace ShowtimeTestingProject
{
    [TestClass]
    public class BookingUnitTest
    {
        [TestMethod]
        public void Details_Booking()
        {
            //setup controller
            var controller = new BookingUnitTestController();

            //call details
            var result = controller.Details(1) as ViewResult;
            var booking = result?.Model as Booking;

            //check booking
            Assert.IsNotNull(booking);
            Assert.AreEqual(1, booking.Id);
            Assert.AreEqual(2, booking.NumberOfTickets);
        }

        [TestMethod]
        public void Create_Booking()
        {
            //setup
            var controller = new BookingUnitTestController();
            var booking = new Booking
            {
                Id = 3,
                NumberOfTickets = 3,
                ShowtimeId = 1,
                UserId = "user3",
                BookingDate = DateTime.Now
            };

            //call create
            var result = controller.Create(booking);

            //check redirect
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirect = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirect?.ActionName);
        }

        [TestMethod]
        public void Delete_Booking()
        {
            //setup
            var controller = new BookingUnitTestController();

            //call delete
            var result = controller.DeleteConfirmed(1);

            //check redirect
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirect = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirect?.ActionName);
        }
    }
}
