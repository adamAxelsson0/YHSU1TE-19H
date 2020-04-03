using System;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Domain.Tests
{
    /// <summary>
    /// In memory booking context fixture.
    /// </summary>
    /// <remarks>
    /// Microsoft.EntityFrameworkCore.InMemory
    /// </remarks>
    public class BookingContextInMemoryFixture : IDisposable
    {
        public BookingContext Context { get; set; }

        public BookingContextInMemoryFixture()
        {
            // Instruct EF to use an in-memory (FAKE!!!) database instance.
            var builder = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase("Bookings");

            Context = new BookingContext(builder.Options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}