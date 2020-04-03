using EFCore.Domain;

namespace EFCore.Infrastructure
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal GetPriceForBookingWith(int userId, int locationId)
        {
            return 0;
        }
    }
}
