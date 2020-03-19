using System;
using DependencyInjectionDemo.Domain.Version4;
using Xunit;
using FakeItEasy;

namespace DependencyInjectionDemo.Domain.Tests.V4
{
    public class BookingServiceTests
    {
        [Fact]
        public void Successful_booking_captures_payment()
        {
            // arrange
            var paymentGateway = A.Fake<IPaymentGateway>();
            var sut = new BookingService(paymentGateway);
            var startTime = DateTime.Now;

            // act
            sut.CreateBooking(new CreateBookingRequest(startTime: startTime, 
                                            durationMinutes: 10,
                                            bookingUser: new User { Id = 1 },
                                            paymentMethod: CreateBookingRequest.BookingPaymentMethod.Swish));

            // assert
            A.CallTo(() => paymentGateway.CapturePayment(10)).MustHaveHappenedOnceExactly();
        }
    }
}