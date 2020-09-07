using BraintreeSample.APIHelper.Validations;
using BraintreeSample.API.Models;
using FluentValidation;

namespace BraintreeSample.API.Validations
{
    public class UserModelValidator : BaseModelValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(255);
            RuleFor(x => x.LastName).MaximumLength(255);
            RuleFor(x => x.UserName).NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(255)
                .Matches("[A-Z]").WithMessage("Password should contain upper case letter.")
                .Matches("[a-z]").WithMessage("Password should contain lower case letter.")
                .Matches("[0-9]").WithMessage("Password should contain number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password should contain special character.");
        }
    }
}