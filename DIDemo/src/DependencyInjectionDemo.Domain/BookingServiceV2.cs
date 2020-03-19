using System;

namespace DependencyInjectionDemo.Domain.Version2
{
    public class CreateBookingRequest
    {
        public CreateBookingRequest(DateTime startTime, int durationMinutes, User bookingUser, BookingPaymentMethod paymentMethod)
        {
            this.StartTime = startTime;
            this.DurationInMinutes = durationMinutes;
            this.BookingUser = bookingUser;
            this.PaymentMethod = paymentMethod;
        }

        public DateTime StartTime { get; }
        public int DurationInMinutes { get; }

        public User BookingUser { get; }

        public BookingPaymentMethod PaymentMethod { get; }
        public enum BookingPaymentMethod
        {
            Klarna,
            CreditCard,
            Swish
        }
    }

    public class BookingService
    {
        private PaymentGateway paymentGateway;

        // expose the payment gateway to allow testing.
        public PaymentGateway PaymentGateway { get { return paymentGateway; } }

        public BookingService(PaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public void CreateBooking(CreateBookingRequest request)
        {
            // calculate price

            // if booking is successful, capture payment
            paymentGateway.CapturePayment(10);
        }
    }

    public class User
    {
        public int Id { get; set; }
    }

    public class PaymentGateway 
    {
        // capture the total payments made to the gateway, to allow testing.
        public decimal TotalPayments {get; private set;}
        public CapturePaymentResponse CapturePayment(decimal amount) {

            TotalPayments += amount;
            // do the hard work
            return new CapturePaymentResponse {
                Result = CapturePaymentResponse.CapturePaymentResult.Success
            };
        }
    }

    public class CapturePaymentResponse
    {

        public CapturePaymentResult Result { get; set; }
        public enum CapturePaymentResult
        {
            Success = 0,
            Failed = 1
        }
    }
}
