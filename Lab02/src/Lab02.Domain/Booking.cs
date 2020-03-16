using System;
using Microsoft.Extensions.Internal;
using NodaTime;

namespace Lab02.Domain
{
    public class Clock : ISystemClock
    {
        private DateTime currentTimeUtc;
        public Clock(DateTime currentTimeUtc)
        {
            this.currentTimeUtc = currentTimeUtc;
        }

        public DateTimeOffset UtcNow => currentTimeUtc;
    }

    public class Booking
    {
        public Party BookingParty { get; }
        public Location Location {get; }
        public bool IsCancelled { get; private set; }
        public DateTime StartTime { get; }
        ISystemClock systemClock;
        private Money basicPrice;
        private Percent vatRate;

        public static Booking Create(DateTime startTime, int durationMinutes, Money price, Percent vatRate, Party bookingParty, Location location, ISystemClock systemClock)
        {
            if (durationMinutes > 60) throw new InvalidOperationException("Bookings must not exceed 60 minutes.");
            if (new LocalTime(startTime.Hour, startTime.Minute) < location.OpeningTime) throw new InvalidOperationException("Bookings must not exceed 60 minutes.");
            return new Booking(startTime, price, vatRate, bookingParty, location, systemClock);
        }

        private Booking(DateTime startTime, Money price, Percent vatRate, Party bookingParty, Location location, ISystemClock systemClock)
        {
            this.StartTime = startTime;
            this.BookingParty = bookingParty;
            this.basicPrice = price;
            this.vatRate = vatRate;
            this.Location = location;
            this.systemClock = systemClock;

            bookingParty.AddBooking(this);
        }

        public TimeSpan Duration { get; }

        public decimal GetPrice() => this.BookingParty switch
        {
            User u when u.Age > 12 && u.Age >= 70 => PensionersPrice,
            User u when u.Age < 12 => ChildPrice,
            Company c => CompanyPrice,
            _ => RegularPrice
        };

        private decimal BasicPriceIncVat => basicPrice + basicPrice * vatRate;
        private decimal PensionersPrice => 0;
        private decimal ChildPrice => BasicPriceIncVat * Percent.FromDecimal(0.5m);
        private decimal CompanyPrice => basicPrice - (basicPrice * ((Company)this.BookingParty).DiscountPercent);
        private decimal RegularPrice => BasicPriceIncVat;

        public void Cancel()
        {
            var timeRemainingUntilBookingTime = systemClock.UtcNow - StartTime;
            if (timeRemainingUntilBookingTime.TotalMinutes < 60)
                throw new InvalidOperationException("Booking may not be cancelled.");

            this.IsCancelled = true;
        }
    }
}