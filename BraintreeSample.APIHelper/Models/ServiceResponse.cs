using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Models
{
    public class ServiceResponse<T>
    {
        public bool IsSuccessed { get; set; }
        public T Data { get; set; }
        public List<ServiceError> Errors { get; set; }
        public ServiceResponse()
        {
            Errors = new List<ServiceError>();
        }
    }
}
