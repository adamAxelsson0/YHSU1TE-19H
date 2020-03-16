using System;
using Xunit;
using FluentAssertions;

namespace Lab02.Domain.Tests
{
    public class CurrencyTests {
        
        [Fact]
        public void Currency_code_should_not_be_null()
        {
            Action a = () => new Currency(null);

            a.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("SEKO")]
        [InlineData("SE")]
        public void Currency_code_should_be_3_characters(string currency)
        {
            Action a = () => new Currency(currency);

            a.Should().Throw<ArgumentException>();
        }
    }
}
