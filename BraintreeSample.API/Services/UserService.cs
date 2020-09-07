using BraintreeSample.APIHelper.Services;
using BraintreeSample.API.Entities;
using BraintreeSample.API.Repositories;
using FluentValidation;
using Serilog;
using BraintreeSample.APIHelper.Models;
using System;
using BraintreeSample.APIHelper.Builders;

namespace BraintreeSample.API.Services
{
    public class UserService : BaseService<UserEntity>
    {
        protected UserRepository _repository;
        public UserService(ILogger logger, UserRepository repository) : base(logger, repository)
        {
            _repository = repository;
        }
        public ServiceResponse<UserEntity> GetValidUserByToken(string username, string token)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<UserEntity>();
            var obj = _repository.GetValidUserByToken(username, token);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "100004", Message = "User token is not valid.", InnerMessage = "User credential is not valid." });
            }
            return serviceResponseBuilder.Ok(obj);
        }
        public ServiceResponse<UserEntity> GetValidUser(string username, string password)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<UserEntity>();
            var obj = _repository.GetValidUser(username, password);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "100001", Message = "User credential is not valid.", InnerMessage = "User credential is not valid." });
            }
            return serviceResponseBuilder.Ok(obj);
        }
        public ServiceResponse<UserEntity> GetByUserName(string username)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<UserEntity>();
            var obj = _repository.GetByUserName(username);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "100002", Message = "Username is not valid.", InnerMessage = "Username is not valid." });
            }
            return serviceResponseBuilder.Ok(obj);
        }
    }
}