using System;
using DependencyInjectionDemo.Domain.Version2;
using Xunit;

namespace DependencyInjectionDemo.Domain.Tests.V2
{
    public class BookingServiceTests
    {
        [Fact]
        public void Successful_booking_captures_payment()
        {
            // arrange
            var paymentGateway = new PaymentGateway();
            var sut = new BookingService(paymentGateway);
            var startTime = DateTime.Now;

            // act
            sut.CreateBooking(new CreateBookingRequest(startTime: startTime, 
                                            durationMinutes: 10,
                                            bookingUser: new User { Id = 1 },
                                            paymentMethod: CreateBookingRequest.BookingPaymentMethod.Swish));

            // assert
            Assert.Equal(10, paymentGateway.TotalPayments);
        }
    }
}