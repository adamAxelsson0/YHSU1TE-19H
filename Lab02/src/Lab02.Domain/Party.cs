using System.Collections.Generic;
using System.Linq;

namespace Lab02.Domain
{
    public class Party
    {
        public List<Booking> RecentBookings { get; } = new List<Booking>();

        public void AddBooking(Booking bookingToAdd)
        {
            if (RecentBookings.Count() > 1)
                RecentBookings.RemoveAt(0);

            RecentBookings.Add(bookingToAdd);
        }
    }
}
