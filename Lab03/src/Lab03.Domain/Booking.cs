using System;
using Microsoft.Extensions.Internal;

namespace Lab03.Domain
{
    public class Booking
    {
        private IPaymentGateway paymentGateway;
        private decimal bookingPrice;
        private ISystemClock systemClock;
        private DateTime BookingDate { get; }

        public Booking(IPaymentGateway paymentGateway, DateTime bookingDate, decimal bookingPrice, ISystemClock systemClock)
        {
            this.paymentGateway = paymentGateway;
            this.BookingDate = bookingDate;
            this.bookingPrice = bookingPrice;
            this.systemClock = systemClock;
        }

        public bool MakeBooking()
        {
            return paymentGateway.CollectPayment();
        }

        /// <summary>
        /// Cancel a booking.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>True, when the booking is successful, otherwise false.</returns>
        public bool CancelBooking()
        {
            int daysToBookingDate = (systemClock.UtcNow - BookingDate).Days;

            return daysToBookingDate switch
            {
                < 60 => return paymentGateway.SendPayment()
            }
        }
    }
}