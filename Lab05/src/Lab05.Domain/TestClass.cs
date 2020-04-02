using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lab05.Domain
{
    public interface IPaymentGateway
    {
        void CapturePayment();
    }

    public class BookingService
    {
        readonly IPaymentGateway paymentGateway;

        public BookingService(IPaymentGateway paymentGateway)
        {
            this.paymentGateway = paymentGateway;
        }

        public void CreateBooking()
        {
            //this.paymentGateway.CapturePayment();
        }
    }

    
    public class TestClass
    {
        public void Test1(string name)
        {

        }

        public void Test2(int quantity)
        {

        }

        public bool Test3(int quantity)
        {
            return quantity >= 0 && quantity <= 10;
        }

        public void CreateBooking(CreateBookingRequest request)
        {

        }

        public void TestDateTime(DateTime date)
        {

        }

        public bool TestYearIs2018(DateTime date)
        {
            return date.Year == 2018;
        }

        public bool StringsAreUniqe(IEnumerable<string> uniqueStrings)
        {
            var duplicates = uniqueStrings.GroupBy(x => x)
                        .Where(group => group.Count() > 1)
                        .Select(group => group.Key);

            return !duplicates.Any();
        }

        public int TwelveStrings(IEnumerable<string> uniqueStrings)
        {
            return uniqueStrings.Count();
        }
    }

    public class CreateBookingRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        
        [Range(1,10)]
        public int Quantity {get;set;}
    }
}