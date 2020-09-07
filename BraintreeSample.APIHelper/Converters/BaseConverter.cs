using BraintreeSample.APIHelper.Entities;
using BraintreeSample.APIHelper.Models;
using System;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Converters
{
    public abstract class BaseConverter<T, Y> : IConverter<T, Y>
        where T : BaseEntity
        where Y : BaseModel
    {
        public BaseConverter()
        {
        }
        public T Convert(Y obj)
        {
            throw new NotImplementedException();
        }
        public Y Convert(T obj)
        {
            throw new NotImplementedException();
        }
        public List<Y> Convert(List<T> objList)
        {
            var returnList = new List<Y>();
            foreach (var obj in objList)
            {
                returnList.Add(Convert(obj));
            }
            return returnList;
        }
        public List<T> Convert(List<Y> objList)
        {
            var returnList = new List<T>();
            foreach (var obj in objList)
            {
                returnList.Add(Convert(obj));
            }
            return returnList;
        }
    }
}
