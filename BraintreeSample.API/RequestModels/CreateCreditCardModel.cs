namespace BraintreeSample.API.RequestModels
{
    public class CreateCreditCardModel
    {
        public string CardNumber { get; set; }
        public string ExpirationYear { get; set; }
        public string ExpirationMonth { get; set; }
        public string Cvv { get; set; }
    }
}
