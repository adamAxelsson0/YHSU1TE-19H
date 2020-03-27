using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab04.Domain
{
    // interface to unmanaged dependency
    public interface IPaymentGateway 
    {
        void CapturePayment();
    }

    // Orchestration
    public class BookingService
    {
        // private - we don't need to access it externally.
        private BookingRepository repository;
        private readonly IPaymentGateway paymentGateway;
        // dependency injection of repository.
        public BookingService(BookingRepository repository, IPaymentGateway paymentGateway)
        {
            this.repository = repository;
            this.paymentGateway = paymentGateway;
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

            // assume everything is ok with the booking
            // usually do more things here
            paymentGateway.CapturePayment();

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

    // Managed dependency
    // fake (simplified database)
    // Encapsulates a complex api to a data store, exposing
    // only those methods we need.
    public class BookingRepository
    {
        // the actual data store (fake).
        // it's private because we want to control what the caller may do.
        public List<Booking> bookings {get;set;} = new List<Booking>();

        // persists the domain object to the data store.
        public void AddBooking(Booking bookingToAdd) {
            this.bookings.Add(bookingToAdd);
        }

        // this method is not required by our service (yet), but is required to be
        // able to test. this is a concession but it's difficult (impossible)
        // to avoid.
        public Booking GetById(string id)
        {
            return this.bookings.FirstOrDefault(s => s.Id == id);
        }
    }

    // booking domain object
    // this is the object persisted to the database
    // it needs at least an id to be able to retrieve it again!
    public class Booking
    {
        public string Id {get;}
        public Booking(string id)
        {
            this.Id = id;
        }
    }
}