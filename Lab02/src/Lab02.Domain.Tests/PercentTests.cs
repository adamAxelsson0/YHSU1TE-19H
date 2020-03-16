using System;
using Xunit;
using FluentAssertions;
using Lab02.Domain;

namespace Lab02.Domain.Tests
{
    public class PercentTests {
        
        [Fact]
        public void Percent_lower_than_zero_is_not_allowed()
        {
            Action a = () => Percent.FromDecimal(-1m);

            a.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Percent values should be between 0 and 1. (Parameter 'percent')");
        }

        [Fact]
        public void Percent_greater_than_1_is_not_allowed()
        {
            Action a = () => Percent.FromDecimal(1.1m);

            a.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Percent values should be between 0 and 1. (Parameter 'percent')");
        }
    }
}
