using NodaTime;

namespace Lab02.Domain
{
    public class Location { 
        public LocalTime OpeningTime {get; }

        public Location(LocalTime openingTime)
        {
            this.OpeningTime = openingTime;
        }
    }
}