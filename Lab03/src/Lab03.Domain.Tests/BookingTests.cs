using System;
using Xunit;
using FakeItEasy;
using FluentAssertions;

namespace Lab03.Domain.Tests
{
    public class BookingTests
    {
        [Fact]
        public void The_booking_should_collect_payment_from_the_users_bank()
        {
            // arrange
            var paymentService = A.Fake<IPaymentGateway>();
            var sut = new BookingService(paymentService);

            // act
            sut.MakeBooking(new CreateBookingRequest(DateTime.Now.AddHours(1)));

            // assert
            A.CallTo(() => paymentService.CollectPayment()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        public void The_booking_is_not_accepted_when_the_payment_fails()
        {
            // arrange
            var paymentService = A.Fake<IPaymentGateway>();
            A.CallTo(() => paymentService.CollectPayment()).Returns(false);

            var sut = new BookingService(paymentService);

            // act
            var result = sut.MakeBooking(new CreateBookingRequest(DateTime.Now.AddHours(1)));

            // assert
            result.Should().BeEquivalentTo(new CreateBookingResponse(CreateBookingResponse.CreateBookingStatus.Failed, "Payment failed."));
        }

        [Fact]
        public void Cancel_the_booking_before_60_day_threshold_is_ok()
        {
            // arrange
            var paymentService = A.Fake<IPaymentGateway>();
            A.CallTo(() => paymentService.CollectPayment()).Returns(false);

            var sut = new BookingService(paymentService);
            long bookingId = 1;

            // act
            var result = sut.CancelBooking(bookingId);

            // assert
            result.Should().BeFalse();
        }
    }
}
