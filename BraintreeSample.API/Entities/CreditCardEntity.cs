using BraintreeSample.APIHelper.Entities;

namespace BraintreeSample.API.Entities
{
	public class CreditCardEntity : BaseEntity
	{
		public int UserId { get; set; }
		public string Token { get; set; }
		public string CardType { get; set; }
		public string BinNumber { get; set; }
		public string LastFour { get; set; }
		public int ExpirationYear { get; set; }
		public int ExpirationMonth { get; set; }
	}
}