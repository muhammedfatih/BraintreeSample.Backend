using BraintreeSample.API.Models;
using BraintreeSample.APIHelper.Validations;
using FluentValidation;

namespace BraintreeSample.API.Validations
{
    public class TransactionModelValidator : BaseModelValidator<TransactionModel>
    {
        public TransactionModelValidator()
        {
            RuleFor(x => x.CreditCardId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}