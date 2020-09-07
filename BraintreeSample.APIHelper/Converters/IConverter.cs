using BraintreeSample.APIHelper.Entities;
using BraintreeSample.APIHelper.Models;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Converters
{
    public interface IConverter<T, Y>
        where T : BaseEntity
        where Y : BaseModel
    {
        T Convert(Y source);
        Y Convert(T source);
        List<T> Convert(List<Y> sourceList);
        List<Y> Convert(List<T> sourceList);
    }
}
