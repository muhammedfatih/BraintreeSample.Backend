using BraintreeSample.API.Entities;
using BraintreeSample.API.Repositories;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Models;
using BraintreeSample.APIHelper.Services;
using Serilog;
using System.Collections.Generic;

namespace BraintreeSample.API.Services
{
    public class TransactionService : BaseService<TransactionEntity>
	{
        protected TransactionRepository _repository;
        public TransactionService(ILogger logger, TransactionRepository repository) : base(logger, repository)
        {
            _repository = repository;
        }
        public ServiceResponse<List<TransactionEntity>> ListForUser(int userId)
        {
            var serviceResponseBuilder = new ServiceResponseBuilder<List<TransactionEntity>>();
            var obj = _repository.ListForUser(userId);
            if (obj == null)
            {
                return serviceResponseBuilder.NotOk(new ServiceError() { Code = "200001", Message = "Credit cards can not be listed.", InnerMessage = "Credit cards can not be listed. Database connection problem has occured." });
            }
            return serviceResponseBuilder.Ok(obj);
        }
    }
}