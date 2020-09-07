using FluentValidation.TestHelper;
using BraintreeSample.APIHelper.Models;
using BraintreeSample.APIHelper.Validations;
using NUnit.Framework;
using System;

namespace BraintreeSample.APIHelper.Tests.Validations
{
    public class BaseEntityValidatorTest
    {
        internal class SampleModel : BaseModel
        {
        }

        [Test]
        public void ItShouldNotReturnValidationErrorWhenCreatedAtIsPositive()
        {
            new BaseModelValidator<SampleModel>().ShouldNotHaveValidationErrorFor(t => t.CreatedAt, DateTime.Now);
        }
    }
}
