using System;

namespace Lab02.Domain
{
    public class Percent
    {
        public decimal Value { get; }
        private Percent(decimal percent)
        {
            if (percent < 0 || percent > 1) throw new ArgumentOutOfRangeException(nameof(percent),
                   "Percent values should be between 0 and 1.");

            this.Value = percent;
        }

        public static Percent FromDecimal(decimal d)
        {
            return new Percent(d);
        }

        public static implicit operator decimal(Percent p) => p.Value;
        public static implicit operator Percent(decimal d) => new Percent(d);
    }
}