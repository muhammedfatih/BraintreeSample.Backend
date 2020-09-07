using FluentValidation.Results;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Tests.Builders
{
    public class ServiceResponseBuilderTest
    {
        [Test]
        public void OkShouldReturnSucceedAndData()
        {
            // Given
            var builder = new ServiceResponseBuilder<int>();

            // When
            var response = builder.Ok(3);

            // Then
            Assert.True(response.IsSuccessed);
            Assert.AreEqual(3, response.Data);
        }
        [Test]
        public void NotOkShouldReturnNotSucceedAndSingleErrorWhenHasSingleError()
        {
            // Given
            var builder = new ServiceResponseBuilder<int>();

            // When
            var response = builder.NotOk(new ServiceError());

            // Then
            Assert.False(response.IsSuccessed);
            Assert.AreEqual(1, response.Errors.Count);
        }
        [Test]
        public void NotOkShouldReturnNotSucceedAndMultipleErrorsWhenHasMultipleErrors()
        {
            // Given
            var builder = new ServiceResponseBuilder<int>();

            // When
            var errors = new List<ServiceError>();
            errors.Add(new ServiceError());
            errors.Add(new ServiceError());
            var response = builder.NotOk(errors);

            // Then
            Assert.False(response.IsSuccessed);
            Assert.AreEqual(2, response.Errors.Count);
        }
        [Test]
        public void NotOkShouldReturnNotSucceedAndSingleErrorWhenHasValidationError()
        {
            // Given
            var builder = new ServiceResponseBuilder<int>();

            // When
            var response = builder.NotOk(new ValidationFailure("", ""));

            // Then
            Assert.False(response.IsSuccessed);
            Assert.AreEqual(1, response.Errors.Count);
        }
        [Test]
        public void NotOkShouldReturnNotSucceedAndMultipleErrorsWhenHasMultipleValidationErrors()
        {
            // Given
            var builder = new ServiceResponseBuilder<int>();

            // When
            var errors = new List<ValidationFailure>();
            errors.Add(new ValidationFailure("", ""));
            errors.Add(new ValidationFailure("", ""));
            var response = builder.NotOk(errors);

            // Then
            Assert.False(response.IsSuccessed);
            Assert.AreEqual(2, response.Errors.Count);
        }
    }
}
