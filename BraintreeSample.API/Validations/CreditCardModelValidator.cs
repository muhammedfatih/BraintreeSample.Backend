using BraintreeSample.APIHelper.Validations;
using BraintreeSample.API.Models;
using FluentValidation;
using System;

namespace BraintreeSample.API.Validations
{
    public class CreditCardModelValidator : BaseModelValidator<CreditCardModel>
    {
        public CreditCardModelValidator()
        {
            RuleFor(x => x.ExpirationYear).GreaterThan(9);
            RuleFor(x => x.BinNumber).MaximumLength(7);
            RuleFor(x => x.LastFour).MaximumLength(4);
            RuleFor(x => x.CardType).MaximumLength(255);
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}