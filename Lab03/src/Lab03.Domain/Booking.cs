namespace Lab03.Domain.Confirm
{
    public interface IPaymentGateway {
        bool CapturePayment(decimal amount, decimal vatAmount);
    }

    public class Price {
        public decimal Amount {get;set;}
        public decimal VatAmount {get;set;}
    }

    public interface IPriceCalculator {
        Price CalculatePrice();
    }

    public interface ILogger {
        void LogSomething(string something);
    }
    public class Booking
    {
        private readonly IPaymentGateway paymentGateway;
        private readonly IPriceCalculator priceCalculator;

        public Booking(IPaymentGateway paymentGateway, IPriceCalculator priceCalculator)
        {
            this.paymentGateway = paymentGateway;
            this.priceCalculator = priceCalculator;
        }

        public void Confirm1()
        {
            this.paymentGateway.CapturePayment(50, 50);
        }

        public void Confirm2()
        {
            this.paymentGateway.CapturePayment(50, 50); 
            this.paymentGateway.CapturePayment(50, 50);
        }

        public void Confirm3()
        {
            this.paymentGateway.CapturePayment(100, 0);
        }

        public void Confirm4()
        {
            this.paymentGateway.CapturePayment(65, 0);
            this.paymentGateway.CapturePayment(50, 0);
        }

        public void Confirm5()
        {
            var result = priceCalculator.CalculatePrice();

            this.paymentGateway.CapturePayment(result.Amount, result.VatAmount);
        }

        public void Confirm6()
        {
            var result1 = priceCalculator.CalculatePrice();

            this.paymentGateway.CapturePayment(result1.Amount, result1.VatAmount);

            var result2 = priceCalculator.CalculatePrice();

            this.paymentGateway.CapturePayment(result2.Amount, result2.VatAmount);
        }

        public void Confirm7()
        {
            this.paymentGateway.CapturePayment(50, 50);
        }
    }
}
