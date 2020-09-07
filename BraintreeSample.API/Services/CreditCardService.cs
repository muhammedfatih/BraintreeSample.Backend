using BraintreeSample.API.Entities;
using BraintreeSample.API.Models;
using BraintreeSample.API.Repositories;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Models;
using BraintreeSample.APIHelper.Services;
using Serilog;
using System;
using System.Collections.Generic;

namespace BraintreeSample.API.Services
{
    public class CreditCardService : BaseService<CreditCardEntity>
	{
        protected CreditCardRepository _repository;
        public CreditCardService(ILogger logger, CreditCardRepository repository) : base(logger, repository)
        {
            _repository = repository;
        }
        public ServiceResponse<List<CreditCardEntity>> ListForUser(int userId)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<List<CreditCardEntity>>();
            var obj = _repository.ListForUser(userId);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "200001", Message = "Credit cards can not be listed.", InnerMessage = "Credit cards can not be listed. Database connection problem has occured." });
            }
            return serviceResponseBuilder.Ok(obj);
        }

        public ServiceResponse<CreditCardEntity> GetByToken(string token)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<CreditCardEntity>();
            var obj = _repository.GetByToken(token);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "200001", Message = "Credit cards can not be listed.", InnerMessage = "Credit cards can not be listed. Database connection problem has occured." });
            }
            return serviceResponseBuilder.Ok(obj);
        }
    }
}