using System;

namespace Lab02.Domain
{
    public class Currency
    {
        public string Value {get;}
        public Currency(string currencyCode)
        {
            if (currencyCode == null) throw new ArgumentNullException(nameof(currencyCode));

            if (currencyCode.Length != 3)
                throw new ArgumentException(nameof(currencyCode));
        }

        public static implicit operator string(Currency c) => c.Value;
        public static implicit operator Currency(string c) => new Currency(c);
    }
}
