using System;
using Xunit;
using FakeItEasy;
using FluentAssertions;
using Autofac.Extras.FakeItEasy;

namespace Lab04.Domain.Tests
{
    public class BookingServiceTests
    {
        [Fact]
        public void Successful_booking_should_be_persisted()
        {
            // arrange
            var id = Guid.NewGuid().ToString();
            var bookingRepository = new BookingRepository();
            var sut = new BookingService(bookingRepository);

            // act
            sut.CreateBooking(new BookingService.CreateBookingRequest(id));

            var booking = bookingRepository.GetById(id);

            // assert
            booking.Should().NotBeNull();
        }
    }
}