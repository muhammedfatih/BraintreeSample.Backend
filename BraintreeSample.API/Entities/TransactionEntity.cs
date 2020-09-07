using BraintreeSample.APIHelper.Entities;

namespace BraintreeSample.API.Entities
{
	public class TransactionEntity : BaseEntity
	{
		public int CreditCardId { get; set; }
		public decimal Amount { get; set; }
	}
}