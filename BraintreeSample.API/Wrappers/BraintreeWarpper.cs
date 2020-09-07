using Braintree;

namespace BraintreeSample.API.Wrappers
{
    public class BraintreeWarpper
    {
        private readonly BraintreeGateway _BraintreeGateway;
        public BraintreeWarpper(BraintreeGateway braintreeGateway)
        {
            _BraintreeGateway = braintreeGateway;
        }
        public string Tokenize(string firstName, string lastName, string userName, string cardNumber, int expirationYear, int expirationMonth, int cvv)
        {
            string token = "";
            var createCustomerRequest = new CustomerRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
            };
            Result<Customer> createCustomerResult = _BraintreeGateway.Customer.Create(createCustomerRequest);
            if (createCustomerResult.Target != null)
            {

                var createCreditCardRequest = new CreditCardRequest
                {
                    CustomerId = createCustomerResult.Target.Id,
                    Number = cardNumber,
                    ExpirationDate = $"{expirationMonth.ToString("00")}/{expirationYear.ToString().Substring(expirationYear.ToString().Length - 2)}",
                    CVV = cvv.ToString("000")
                };

                var createCreditCardResult = _BraintreeGateway.CreditCard.Create(createCreditCardRequest);
                if (createCreditCardResult.Target != null)
                {
                    CreditCard creditCard = createCreditCardResult.Target;
                    token = creditCard.Token;
                }
            }
            return token;
        }
        public bool Sale(decimal amount, string token)
        {
            var transactionRequest = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodToken = token
            };

            Result<Transaction> transactionResult = _BraintreeGateway.Transaction.Sale(transactionRequest);
            return transactionResult.Target != null;
        }
    }
}
