using System;
using FluentAssertions;
using Xunit;
using AutoFixture;
using AutoFixture.Xunit2;
using FakeItEasy;
using System.ComponentModel.DataAnnotations;

namespace Lab05.Domain.Tests
{
    public class AutofixtureTests 
    {
        [Fact]
        public void Create_a_string_fixture()
        {
            // arrange
            var fixture = new Fixture();
            var name = fixture.Create<string>();
            var sut = new TestClass();

            // act
            sut.Test1(name);

            // assert
        }

        [Fact]
        public void Create_an_random_int_fixture()
        {
            // arrange
            var fixture = new Fixture();
            var quantity = fixture.Create<int>();
            var sut = new TestClass();

            // act
            sut.Test2(quantity);

            // assert
        }

        [Fact]
        public void Create_a_range_int_fixture()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customizations.Add(new RandomNumericSequenceGenerator(0, 10));
            var quantity = fixture.Create<int>();
            var sut = new TestClass();

            // act
            sut.Test3(quantity);

            // assert
        }

        [Fact]
        public void Create_a_datetime_value()
        {
            // arrange
            var fixture = new Fixture();
            var date = fixture.Create<DateTime>();
            var sut = new TestClass();


            var min = DateTime.MinValue;
            var max = DateTime.MaxValue;

            // act
            sut.TestDateTime(date);

            // assert
        }

        [Fact]
        public void Create_a_datetime_value_during_2018()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customizations.Add(new RandomDateTimeSequenceGenerator(new DateTime(2018, 01, 01), new DateTime(2018, 12, 31)));
            var date = fixture.Create<DateTime>();
            var sut = new TestClass();

            // act
            var yearIs2018 = sut.TestYearIs2018(date);

            // assert
            yearIs2018.Should().BeTrue();
        }

        [Fact]
        public void Create_a_string_with_prefix_fixture()
        {
            // arrange
            var fixture = new Fixture();
            var name = fixture.Create<string>("Jason");
            var nameAlt = $"Jason{name}";

            var sut = new TestClass();

            // act
            sut.Test1(name);

            // assert
            name.Substring(0, 5).Should().Be("Jason");
        }

        [Fact]
        public void Create_a_list_of_strings()
        {
            // arrange
            var fixture = new Fixture();
            var listOfStrings = fixture.CreateMany<string>("Jason");
            var sut = new TestClass();

            // act
            var wereUnique = sut.StringsAreUniqe(listOfStrings);

            // assert
            wereUnique.Should().BeTrue();
        }

        [Fact]
        public void Create_a_list_of_strings_length_12()
        {
            // arrange
            var fixture = new Fixture() { RepeatCount = 12 };
            var listOfStrings = fixture.CreateMany<string>("Prefix_");
            var sut = new TestClass();

            // act
            var count = sut.TwelveStrings(listOfStrings);

            // assert
            count.Should().Be(12);
        }

        [Theory, AutoData]
        public void Create_an_int_using_autodata(int i)
        {
            // arrange
            var sut = new TestClass();

            // act
            sut.Test2(i);

            // assert
        }

        [Theory, AutoData]
        public void Create_a_string_using_autodata(string i)
        {
            // arrange
            var sut = new TestClass();

            // act
            sut.Test1(i);

            // assert
        }

        [Fact]
        public void Create_a_complex_object()
        {
            // arrange
            var fixture = new Fixture();
            var request = fixture.Create<CreateBookingRequest>();

            var sut = new TestClass();

            // act
            sut.CreateBooking(request);

            // assert
        }

        [Fact]
        public void Create_a_complex_object_with_string_override()
        {
            // arrange
            var fixture = new Fixture();
            var request = fixture.Build<CreateBookingRequest>()
                        .With(s => s.Name, "Jason")
                        .With(s => s.Location, "MyLocation")
                        .Create();

            var sut = new TestClass();

            // act
            sut.CreateBooking(request);

            // assert
            request.Name.Should().Be("Jason");
        }

        [Fact]
        public void Create_a_complex_object_with_default_string_override()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Register<string>(() => "Jason");
            var request = fixture.Create<CreateBookingRequest>();

            var sut = new TestClass();

            // act
            sut.CreateBooking(request);

            // assert
            request.Location.Should().Be("Jason");
            request.Name.Should().Be("Jason");
        }

        [Fact]
        public void Create_a_complex_object_with_name_left_as_null()
        {
            // arrange
            var fixture = new Fixture();
            var request = fixture.Build<CreateBookingRequest>()
                            .Without(s => s.Name)
                            .Create();

            var sut = new TestClass();

            // act
            sut.CreateBooking(request);

            // assert
            request.Name.Should().BeNull();
        }

        [Theory, AutoFakeItEasy]
        public void Pass_in_a_fake_interface([Frozen]IPaymentGateway paymentGateway, BookingService sut)
        {
            // arrange
            // not much here!!

            // act
            sut.CreateBooking();

            // assert
            A.CallTo(() => paymentGateway.CapturePayment()).MustHaveHappened();
        }

        [Theory, AutoFakeItEasy]
        public void Limit_int_range_revisited(CreateBookingRequest request)
        {
            // arrange
            // not much here!!

            // act
            request.Quantity.Should().BeInRange(1, 10);
        }
    }
}