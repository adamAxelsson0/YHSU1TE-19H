namespace Lab02.Domain
{
    public class Price
    {
        public Money Amount { get; set; }
        public Percent Vat {get;set;}

        public Price(Money amount, Percent vat)
        {
            this.Amount = amount;
            this.Vat = vat;
        }
    }
}
