using System.Collections.Generic;
using MongoDB.Driver;

namespace Lab04.Domain
{
    // Managed dependency
    // MongoDB
    // Encapsulates a complex api to a data store, exposing
    // only those methods we need.
    public class BookingRepositoryMongoDB
    {
        private readonly IMongoClient mongoClient;
        private IMongoDatabase database;

        public BookingRepositoryMongoDB(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            this.database = mongoClient.GetDatabase("Bookings");
        }

        // persists the domain object to the data store.
        public void AddBooking(Booking bookingToAdd) {
            var collection = this.database.GetCollection<Booking>("Bookings");
            collection.InsertOne(bookingToAdd);
        }

        // this method is not required by our service (yet), but is required to be
        // able to test. this is a concession but it's difficult (impossible)
        // to avoid.
        public Booking GetById(string id)
        {
            var collection = this.database.GetCollection<Booking>("Bookings");
            var filterBuilder = Builders<Booking>.Filter;
            var filter = filterBuilder.Eq(s => s.Id, id);

            return collection.Find(filter).FirstOrDefault();
        }
    }
}