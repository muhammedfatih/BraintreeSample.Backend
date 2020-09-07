using FluentValidation.Results;
using BraintreeSample.APIHelper.Models;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Converters
{
    public class ValidationFailureConverter
    {
        public ServiceError Convert(ValidationFailure source)
        {
            return new ServiceError()
            {
                Code = source.ErrorCode,
                Message = source.ErrorMessage,
                InnerMessage = source.ErrorMessage
            };
        }
        public List<ServiceError> Convert(List<ValidationFailure> source)
        {
            var serviceErrors = new List<ServiceError>();
            foreach (var item in source)
            {
                serviceErrors.Add(this.Convert(item));
            }
            return serviceErrors;
        }
    }
}
