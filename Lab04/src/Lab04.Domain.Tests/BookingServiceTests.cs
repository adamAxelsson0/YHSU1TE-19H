using System;
using Xunit;
using FakeItEasy;
using FluentAssertions;
using Autofac.Extras.FakeItEasy;

namespace Lab04.Domain.Tests
{
    public class BookingServiceTests
    {
        [Fact] // state based (black box)
        public void Successful_booking_should_be_persisted()
        {
            // arrange
            var id = Guid.NewGuid().ToString();
            var bookingRepository = new BookingRepository();
            // unmanaged dependency - interface
            var paymentGateway = A.Fake<IPaymentGateway>();
            var sut = new BookingService(bookingRepository, paymentGateway);

            // act
            sut.CreateBooking(new BookingService.CreateBookingRequest(id, "jason"));

            var booking = bookingRepository.GetById(id);

            // assert
            booking.Id.Should().Be(id);
        }

        [Fact] // behavioural or communication (white box)
        public void Successful_booking_should_capture_payment()
        {
            // arrange
            var id = Guid.NewGuid().ToString();
            // managed dependency - concrete instance
            var bookingRepository = new BookingRepository();
            // unmanaged dependency - interface
            var paymentGateway = A.Fake<IPaymentGateway>();

            var sut = new BookingService(bookingRepository, paymentGateway);

            // act
            sut.CreateBooking(new BookingService.CreateBookingRequest(id, "jason"));

            // assert
            A.CallTo(() => paymentGateway.CapturePayment()).MustHaveHappened();
        }
    }
}