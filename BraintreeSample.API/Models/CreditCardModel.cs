using BraintreeSample.APIHelper.Models;

namespace BraintreeSample.API.Models
{
	public class CreditCardModel : BaseModel
	{
		public int UserId { get; set; }
		public UserModel User { get; set; }
		public string Token { get; set; }
		public string CardType { get; set; }
		public string BinNumber { get; set; }
		public string LastFour { get; set; }
		public int ExpirationYear { get; set; }
		public int ExpirationMonth { get; set; }
		public CreditCardModel () {
			User = new UserModel();
		}
	}
}