using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Fundamentals;
using TestNinja.Mocking;

namespace TestNinja.UnitTest.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Booking _existingbooking;
        private Mock<IBookingRepo> _mock;

        [SetUp]
        public void SetUp()
        {
            _existingbooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2021, 1, 15),
                DepartureDate = DepartOn(2021, 1, 20),
                Reference = "a"

            };

            _mock = new Mock<IBookingRepo>();

            _mock.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingbooking

            }.AsQueryable());

        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int day = 1)
        {
            return dateTime.AddDays(day);
        }


        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingbooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingbooking.ArrivalDate),
            }, _mock.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.ArrivalDate),
            }, _mock.Object);

            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.DepartureDate),
            }, _mock.Object);

            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.ArrivalDate),
                DepartureDate = Before(_existingbooking.DepartureDate),
            }, _mock.Object);

            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }
        
        [Test]
        public void BookingStartsInMiddleOfAnExistingAndFinishesAfter_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.DepartureDate),
            }, _mock.Object);
            
            Assert.That(result, Is.EqualTo(_existingbooking.Reference));
        }
        
        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.DepartureDate),
                DepartureDate = After(_existingbooking.DepartureDate, day:2),
            }, _mock.Object);
            
            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void BookingsOvewrlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingbooking.ArrivalDate),
                DepartureDate = After(_existingbooking.DepartureDate),
                Status = "Cancelled"
            }, _mock.Object);
            
            Assert.That(result, Is.Empty);
        }

    }
}