using BraintreeSample.APIHelper.Models;

namespace BraintreeSample.API.Models
{
	public class TransactionModel : BaseModel
	{
		public int CreditCardId { get; set; }
		public CreditCardModel CreditCardModel { get; set; }
		public decimal Amount { get; set; }
		public TransactionModel()
		{
			CreditCardModel = new CreditCardModel();
		}
	}
}