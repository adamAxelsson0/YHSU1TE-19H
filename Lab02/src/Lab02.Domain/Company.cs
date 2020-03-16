namespace Lab02.Domain
{
    public class Company : Party
    {
        public decimal DiscountPercent { get; }

        public Company(Percent discount)
        {
            this.DiscountPercent = discount;
        }
    }
}
