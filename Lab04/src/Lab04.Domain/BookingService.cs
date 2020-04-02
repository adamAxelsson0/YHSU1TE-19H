using System;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Lab04.Domain
{
    // interface to unmanaged dependency
    public interface IPriceCalculator 
    {
        decimal GetPriceForBookingWith(int userId, int locationId);
    }

    // interface to unmanaged dependency
    public interface IPaymentGateway 
    {
        bool CapturePayment(decimal totalAmount);
    }

    // Orchestration
    public class BookingService
    {
        // private - we don't need to access it externally.
        private BookingRepositoryMongoDB repository;
        private readonly IPaymentGateway paymentGateway;
        private readonly IPriceCalculator priceCalculator;

        // dependency injection of repository.
        public BookingService(BookingRepositoryMongoDB repository, 
                            IPaymentGateway paymentGateway,
                            IPriceCalculator priceCalculator)
        {
            this.repository = repository;
            this.paymentGateway = paymentGateway;
            this.priceCalculator = priceCalculator;
        }

        // create a new booking and persist it to our fake database.
        public void CreateBooking(CreateBookingRequest request)
        {
            // not part of the test, but is an example of why
            // we use a separate CreateBookingRequest object
            // than the domain object Booking itself as parameter
            // the request has properties that aren't relevant
            // to persist in the data store.
            if (!HasPermission(request.RequestedBy))
                throw new InvalidOperationException("You don't have permission.");

            // get the price from the price calculator
            var totalAmount = priceCalculator.GetPriceForBookingWith(1, 2);

            // assume everything is ok with the booking
            // usually do more things here
            var capturePaymentResult = paymentGateway.CapturePayment(totalAmount);

            if (capturePaymentResult == true)
                repository.AddBooking(new Booking(request.Id));
        }

        // Input parameters to method CreateBooking
        // data transfer object (dto) that collects
        // all the required input to the booking method
        public class CreateBookingRequest
        {
            public string Id { get; }
            public string RequestedBy {get;}
            public CreateBookingRequest(string id, string requestedBy)
            {
                this.Id = id;
                this.RequestedBy = requestedBy;
            }
        }

        // No, ABBA! Denied!
        private bool HasPermission(string requestedBy)
        {
            var bandMembers = new string[] {"Björn", "Benny", "Agnetha", "Anni-Frid" };

            return !bandMembers.Contains(requestedBy);
        }
    }

    // booking domain object
    // this is the object persisted to the database
    // it needs at least an id to be able to retrieve it again!
    public class Booking
    {
        [BsonId] // tell mongo which property is the unique id.
        public string Id {get;}

        [BsonConstructor]
        public Booking(string id)
        {
            this.Id = id;
        }
    }
}