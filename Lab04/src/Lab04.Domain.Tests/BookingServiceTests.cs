using System;
using Xunit;
using FakeItEasy;
using FluentAssertions;
using Autofac.Extras.FakeItEasy;
using MongoDB.Driver;

namespace Lab04.Domain.Tests
{
    public class BookingServiceTests : MongoDbIntegrationTest
    {
        [Fact] // state based (black box)
        public void Successful_booking_should_be_persisted()
        {
            // arrange
            var id = Guid.NewGuid().ToString();
            var bookingRepository = new BookingRepositoryMongoDB(this.mongoClient);
            // unmanaged dependency - interface
            var paymentGateway = A.Fake<IPaymentGateway>();
            var priceCalculator = A.Fake<IPriceCalculator>();
            var sut = new BookingService(bookingRepository, paymentGateway, priceCalculator);

            // act
            sut.CreateBooking(new BookingService.CreateBookingRequest(id, "jason"));

            var booking = bookingRepository.GetById(id);

            // assert
            booking.Id.Should().Be(id);
        }

        //[Fact] // behavioural or communication (white box)
        public void Successful_booking_should_capture_payment()
        {
            // arrange
            var id = Guid.NewGuid().ToString();
            // managed dependency - concrete instance
            var bookingRepository = new BookingRepositoryMongoDB(this.mongoClient);
            // unmanaged dependency - interface
            var priceCalculator = A.Fake<IPriceCalculator>();
            var paymentGateway = A.Fake<IPaymentGateway>();

            var sut = new BookingService(bookingRepository, paymentGateway, priceCalculator);

            // act
            sut.CreateBooking(new BookingService.CreateBookingRequest(id, "jason"));

            // assert
            A.CallTo(() => paymentGateway.CapturePayment(A<decimal>.Ignored)).MustHaveHappened();
        }

        //[Fact] // behavioural or communication (white box)
        public void Should_use_total_price_from_price_service()
        {
            // arrange
            var auto = new AutoFake();
            var priceCalculator = auto.Resolve<IPriceCalculator>();
            var paymentGateway = auto.Resolve<IPaymentGateway>();
            auto.Provide(this.mongoClient);

            var id = Guid.NewGuid().ToString();

            // use price calculator as stub
            A.CallTo(() => priceCalculator
                .GetPriceForBookingWith(A<int>.Ignored, A<int>.Ignored, A<DateTime>.Ignored))
                .Returns(100);

            var sut = auto.Resolve<BookingService>();

            // act
            sut.CreateBooking(new BookingService.CreateBookingRequest(id, "jason"));

            // assert
            A.CallTo(() => paymentGateway.CapturePayment(100)).MustHaveHappened();
        }
    }
}