using FluentValidation;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Entities;
using BraintreeSample.APIHelper.Models;
using BraintreeSample.APIHelper.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BraintreeSample.APIHelper.Services
{
    public abstract class BaseService<T> : IService<T>
        where T : BaseEntity
    {
        private readonly ILogger _logger;
        protected readonly IRepository<T> _repository;
        public BaseService(ILogger logger, IRepository<T> repository)
        {
            this._logger = logger;
            this._repository = repository;
        }
        public ServiceResponse<T> Create(T obj)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<T>();
            var returnId = _repository.Create(obj);
            if (returnId > 0)
            {
                return this.Read(returnId);
            }
            else
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "", Message = "", InnerMessage = "" });
            }
        }
        public ServiceResponse<T> Read(int id)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<T>();
            var obj = _repository.Read(id);
            if (obj == null)
            {
                _logger.Error("{source} {template} {logtype} {ExceptionMessage}", "service", typeof(T).Name, "serviceerror", $"{typeof(T)} with id {id} not found.");
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "", Message = "", InnerMessage = "" });
            }
            return serviceResponseBuilder.Ok(obj);
        }
        public ServiceResponse<T> Read(Guid guid)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<T>();
            var obj = _repository.Read(guid);
            if (obj == null)
            {
                _logger.Error("{source} {template} {logtype} {ExceptionMessage}", "service", typeof(T).Name, "serviceerror", $"{typeof(T)} with guid {guid} not found.");
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "", Message = "", InnerMessage = "" });
            }
            return serviceResponseBuilder.Ok(obj);

        }
        public ServiceResponse<T> Update(T obj)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<T>();
            var result = _repository.Update(obj);
            if (result)
            {
                ServiceResponse<T> readObj = this.Read(obj.ID);
                if (obj.ID == 0) readObj = this.Read(new Guid(obj.Guid));
                return readObj;
            }
            else
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "", Message = "", InnerMessage = "" });
            }
        }
        public ServiceResponse<bool> Delete(int id)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<bool>();
            var result = _repository.Delete(id);
            if (!result)
            {
                _logger.Error("{source} {template} {logtype} {ExceptionMessage}", "service", typeof(T).Name, "serviceerror", $"{typeof(T)} with id {id} not deleted.");
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "", Message = "", InnerMessage = "" });
            }
            return serviceResponseBuilder.Ok(true);
        }
        public ServiceResponse<bool> Delete(Guid guid)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<bool>();
            var result = _repository.Delete(guid);
            if (!result)
            {
                _logger.Error("{source} {template} {logtype} {ExceptionMessage}", "service", typeof(T).Name, "serviceerror", $"{typeof(T)} with guid {guid} not deleted.");
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "", Message = "", InnerMessage = "" });
            }
            return serviceResponseBuilder.Ok(true);
        }
        public ServiceResponse<List<T>> List(int page, int pageSize)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<List<T>>();
            var result = _repository.List(page, pageSize);
            return serviceResponseBuilder.Ok(result);
        }
    }
}
