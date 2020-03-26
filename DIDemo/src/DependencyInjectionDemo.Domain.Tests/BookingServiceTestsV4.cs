using System;
using DependencyInjectionDemo.Domain.Version4;
using Xunit;
using FakeItEasy;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionDemo.Domain.Tests.V4
{
    public class BookingServiceTests
    {
        // [Fact]
        // public void Successful_booking_captures_payment()
        // {
        //     // arrange
        //     var paymentGateway = A.Fake<IPaymentGateway>();
        //     var sut = new BookingService(paymentGateway);
        //     var startTime = DateTime.Now;

        //     // act
        //     sut.CreateBooking(new CreateBookingRequest(startTime: startTime, 
        //                                     durationMinutes: 10,
        //                                     bookingUser: new User { Id = 1 },
        //                                     paymentMethod: CreateBookingRequest.BookingPaymentMethod.Swish));

        //     // assert
        //     A.CallTo(() => paymentGateway.CapturePayment(19)).MustHaveHappenedOnceExactly();
        // }

        [Fact]
        public void Functional_test()
        {
            // arrange
            var functionTest = new FunctionTest();

            // act
            var result = functionTest.Add(1, 3);

            // assert
            Assert.Equal(4, result);
        }

        [Fact]
        public void State_based_test()
        {
            // arrange
            var order = new Order(null);

            // act
            order.AddLineItem(productId: 1, price: 3);

            // assert
            Assert.Equal(1, order.TotalLineItems);
        }

        [Fact]
        public void State_based_test_should_capture_payment()
        {
            // arrange
            var paymentGateway = new PaymentGatewayFake();
            var sut = new Order(paymentGateway);
            sut.AddLineItem(10, 1);

            // act
            var result = sut.ConfirmOrder();

            // assert
           Assert.Equal(10, paymentGateway.TotalPaymentsCaptured);
        }

        public class FunctionTest {

            public int Add(int i, int j) {
                return i + j;
            }
        }

        public class PaymentGatewayFake : IPaymentGateway
        {
            public decimal TotalPaymentsCaptured { get; private set; }
            public CapturePaymentResponse CapturePayment(decimal amount)
            {
                TotalPaymentsCaptured += amount;
                return new CapturePaymentResponse();
            }
        }

        public class Order {
            private List<LineItem> lineItems = new List<LineItem>();

            public int TotalLineItems { get { return lineItems.Count; } }
            private readonly IPaymentGateway paymentGateway;

            public OrderStatus Status {get; private set;}
            public enum OrderStatus {
                Created,
                Confirmed
            }
            public Order(IPaymentGateway paymentGateway) {
                this.paymentGateway = paymentGateway;
                this.Status = OrderStatus.Created;
            }

            // Confirm the order, setting the status to confirmed.
            public virtual bool ConfirmOrder() {
              //  this.paymentGateway.CapturePayment(lineItems.Sum(s => s.Amount));
                this.Status = OrderStatus.Confirmed;
                return true;
            }

            public decimal TotalOrderValue {get { return lineItems.Sum(s => s.Amount); }}
            public void AddLineItem(decimal price, int productId) {
                lineItems.Add(new LineItem { Amount = price, ProductId = productId});
            }

            public class LineItem {
                public int ProductId {get;set;}
                public decimal Amount {get;set;}
            }    
        }
    }
}