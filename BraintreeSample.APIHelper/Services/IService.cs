using BraintreeSample.APIHelper.Entities;
using BraintreeSample.APIHelper.Models;
using System;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Services
{
    public interface IService<T>
        where T : BaseEntity
    {
        ServiceResponse<T> Create(T obj);
        ServiceResponse<T> Read(int id);
        ServiceResponse<T> Read(Guid guid);
        ServiceResponse<T> Update(T obj);
        ServiceResponse<bool> Delete(int id);
        ServiceResponse<bool> Delete(Guid guid);
        ServiceResponse<List<T>> List(int page, int pageSize);
    }
}
