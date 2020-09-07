using BraintreeSample.APIHelper.Entities;
using System;
using System.Collections.Generic;

namespace BraintreeSample.APIHelper.Repositories
{
    public interface IRepository<T>
    where T : BaseEntity
    {
        int Create(T obj);
        T Read(int id);
        T Read(Guid guid);
        bool Update(T obj);
        bool Delete(int id);
        bool Delete(Guid guid);
        List<T> List(int page, int pageSize);
    }
}
