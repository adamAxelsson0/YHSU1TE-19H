namespace Lab02.Domain
{

    public class Money
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        private Money(Currency currency, decimal amount)
        {
            this.Currency = currency;
            this.Amount = amount;
        }

        public static Money Create(Currency currency, decimal amount)
        {
            return new Money(currency, amount);
        }

        public static implicit operator decimal(Money m) => m.Amount;
    }
}
