using FluentValidation.Results;
using BraintreeSample.APIHelper.Converters;
using BraintreeSample.APIHelper.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Tests.Converters
{
    public class ValidationFailureConveterTest
    {
        [Test]
        public void ItShouldReturnValidServiceError()
        {
            // Given
            ValidationFailure failure = new ValidationFailure("", "error message")
            {
                ErrorCode = "101",
            };
            ValidationFailureConverter converter = new ValidationFailureConverter();

            // When
            ServiceError error = converter.Convert(failure);

            // Then
            Assert.AreEqual("101", error.Code);
            Assert.AreEqual("error message", error.Message);
            Assert.AreEqual("error message", error.InnerMessage);
        }
        [Test]
        public void ItShouldReturnValidListOfServiceError()
        {
            // Given
            var failures = new List<ValidationFailure>();
            failures.Add(new ValidationFailure("", "error message")
            {
                ErrorCode = "101",
            });
            failures.Add(new ValidationFailure("", "error message2")
            {
                ErrorCode = "102",
            });

            ValidationFailureConverter converter = new ValidationFailureConverter();

            // When
            List<ServiceError> errors = converter.Convert(failures);

            // Then
            Assert.AreEqual(2, errors.Count);
            Assert.AreEqual("101", errors[0].Code);
            Assert.AreEqual("error message", errors[0].Message);
            Assert.AreEqual("error message", errors[0].InnerMessage);
            Assert.AreEqual("102", errors[1].Code);
            Assert.AreEqual("error message2", errors[1].Message);
            Assert.AreEqual("error message2", errors[1].InnerMessage);
        }
    }
}
