using BraintreeSample.APIHelper.Models;

namespace BraintreeSample.API.Models
{
	public class UserModel : BaseModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
	}
}