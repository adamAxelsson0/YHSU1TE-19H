using Lab02.Domain;
using FluentAssertions;
using System;
using Xunit;
using NodaTime;

namespace Lab02.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void Recent_bookings_limited_to_most_recent_two()
        {
            // arrange
            var sut = new User(20);

            // act
            var booking1 = CreateBooking(sut);
            CreateBooking(sut);
            var booking3 = CreateBooking(sut);

            // assert
            sut.RecentBookings.Should().HaveCount(2);
            sut.RecentBookings.Should().NotContain(booking1);
        }

        private Booking CreateBooking(User user)
        {
            return Booking.Create(DateTime.Now, 1, Money.Create("SEK", 50), 0.25m, user, new Location(new LocalTime(07, 00)), new Clock(DateTime.Now));
        }
    }
}