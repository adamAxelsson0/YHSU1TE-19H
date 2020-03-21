using System;
using Xunit;
using Moq;
using Autofac.Extras.Moq;

namespace Lab03.Domain.Confirm.Tests
{
    public class BookingServiceTests
    {
        //[Fact]
        //public void Booking_confirm_should_take_a_payment()
        //{
        //    // arrange
        //    var paymentGateway = new Mock<IPaymentGateway>();
        //    var priceCalculator = new Mock<IPriceCalculator>();
        //    var booking = new Booking(paymentGateway.Object, priceCalculator.Object);

        //    // act
        //    booking.Confirm1();

        //    // assert
        //    paymentGateway.Verify(s => s.CapturePayment(It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Once);

        //}

        //[Fact]
        //public void Booking_confirm_should_take_two_payments_with_amount_50()
        //{
        //    // arrange
        //    var paymentGateway = new Mock<IPaymentGateway>();
        //    var priceCalculator = new Mock<IPriceCalculator>();
        //    var booking = new Booking(paymentGateway.Object, priceCalculator.Object);

        //    // act
        //    booking.Confirm2();

        //    // assert
        //    paymentGateway.Verify(s => s.CapturePayment(50, It.IsAny<decimal>()), Times.Exactly(2));
        //}

        //[Fact]
        //public void Booking_confirm_should_take_one_payment_with_amount_100_and_any_vat()
        //{
        //    // arrange
        //    var paymentGateway = new Mock<IPaymentGateway>();
        //    var priceCalculator = new Mock<IPriceCalculator>();
        //    var booking = new Booking(paymentGateway.Object, priceCalculator.Object);

        //    // act
        //    booking.Confirm3();

        //    // assert
        //    paymentGateway.Verify(s => s.CapturePayment(100, It.IsAny<decimal>()), Times.Once);
        //}

        //[Fact]
        //public void Booking_should_take_one_payment_with_amount_50_and_another_with_amount_65()
        //{
        //    // arrange
        //    var paymentGateway = new Mock<IPaymentGateway>();
        //    var priceCalculator = new Mock<IPriceCalculator>();
        //    var booking = new Booking(paymentGateway.Object, priceCalculator.Object);

        //    // act
        //    booking.Confirm4();

        //    // assert
        //    paymentGateway.Verify(s => s.CapturePayment(50, It.IsAny<decimal>()), Times.Once);
        //    paymentGateway.Verify(s => s.CapturePayment(65, It.IsAny<decimal>()), Times.Once);
        //}

        //[Fact]
        //public void Booking_confirm_should_use_the_amount_suggested_by_the_price_calculator()
        //{
        //    // arrange
        //    var paymentGateway = new Mock<IPaymentGateway>();
        //    var priceCalculator = new Mock<IPriceCalculator>();
        //    var booking = new Booking(paymentGateway.Object, priceCalculator.Object);
        //    var amount = 50m;
        //    var vatAmount = 2.5m;

        //    priceCalculator.Setup(s => s.CalculatePrice()).Returns(new Price { Amount = amount, VatAmount = vatAmount });

        //    // act
        //    booking.Confirm5();

        //    // assert
        //    paymentGateway.Verify(s => s.CapturePayment(amount,vatAmount), Times.Once);
        //}


        [Fact]
        public void Booking_confirm_should_take_two_different_payments_suggested_by_the_price_calculator()
        {
            // arrange
            using var mock = AutoMock.GetLoose();

            var booking = mock.Create<Booking>();
            var priceCalculator = mock.Mock<IPriceCalculator>();
            var paymentGateway = mock.Mock<IPaymentGateway>();

            var price1 = new Price { Amount = 100, VatAmount = 5m };
            var price2 = new Price { Amount = 50, VatAmount = 2.5m };

            priceCalculator.SetupSequence(s => s.CalculatePrice())
                .Returns(price1)
                .Returns(price2);

            // act
            booking.Confirm6();

            // assert
            paymentGateway.Verify(s => s.CapturePayment(price1.Amount, price1.VatAmount), Times.Once);
            paymentGateway.Verify(s => s.CapturePayment(price2.Amount, price2.VatAmount), Times.Once);
        }
    }
}
