using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using FluentAssertions;
using static EFCore.Domain.BookingService;
using Autofac.Extras.FakeItEasy;
using EFCore.Domain;
using FakeItEasy;

namespace EFCore.AspNet.Tests
{
    public class BookingApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        CustomWebApplicationFactory<Startup> factory;
        HttpClient client;
        AutoFake autoFake;

        public BookingApiTests(
             CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            this.autoFake = factory.AutoFake;
        }

        /// <summary>
        /// Provides good resistance to regression.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Happy path!
        /// </remarks>
        [Fact]
        public async Task Create_booking()
        {
            // arrange
            var paymentGateway = autoFake.Resolve<IPaymentGateway>();

            // stub
            A.CallTo(() => paymentGateway.CapturePayment(A<decimal>.Ignored)).Returns(true);

            var json = JsonConvert.SerializeObject(new CreateBookingRequest(Guid.NewGuid().ToString(), "Jason", DateTime.Now), new JsonSerializerSettings());

            using var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            // act
            var result = await client.PostAsync("/bookings", requestContent);

            // assert
            result.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
