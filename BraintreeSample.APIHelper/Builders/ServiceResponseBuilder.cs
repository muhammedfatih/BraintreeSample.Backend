using FluentValidation.Results;
using BraintreeSample.APIHelper.Converters;
using BraintreeSample.APIHelper.Models;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Builders
{
    public class ServiceResponseBuilder<T>
    {
        public ServiceResponse<T> Ok(T data)
        {
            return new ServiceResponse<T>()
            {
                IsSuccessed = true,
                Data = data
            };
        }
        public ServiceResponse<T> NotOk(ServiceError serviceError)
        {
            var serviceResponse = new ServiceResponse<T>()
            {
                IsSuccessed = false
            };
            serviceResponse.Errors.Add(serviceError);
            return serviceResponse;
        }
        public ServiceResponse<T> NotOk(List<ServiceError> serviceError)
        {
            var serviceResponse = new ServiceResponse<T>()
            {
                IsSuccessed = false
            };
            serviceResponse.Errors.AddRange(serviceError);
            return serviceResponse;
        }
        public ServiceResponse<T> NotOk(ValidationFailure serviceError)
        {
            var serviceResponse = new ServiceResponse<T>()
            {
                IsSuccessed = false
            };
            serviceResponse.Errors.Add(new ValidationFailureConverter().Convert(serviceError));
            return serviceResponse;
        }
        public ServiceResponse<T> NotOk(List<ValidationFailure> serviceError)
        {
            var serviceResponse = new ServiceResponse<T>()
            {
                IsSuccessed = false
            };
            serviceResponse.Errors.AddRange(new ValidationFailureConverter().Convert(serviceError));
            return serviceResponse;
        }
    }
}
