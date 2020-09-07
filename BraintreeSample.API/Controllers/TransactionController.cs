using Braintree;
using BraintreeSample.API.Converters;
using BraintreeSample.API.Middleware;
using BraintreeSample.API.Models;
using BraintreeSample.API.RequestModels;
using BraintreeSample.API.Services;
using BraintreeSample.API.Wrappers;
using BraintreeSample.APIHelper.Converters;
using BraintreeSample.APIHelper.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BraintreeSample.API.Controllers
{
    [Route("api/transaction")]
    [AFAuthorization]
    public class TransactionController : BaseController
    {
        private readonly CreditCardService _creditCardService;
        private readonly TransactionService _transactionService;
        private readonly AbstractValidator<TransactionModel> _transactionModelValidator;
        private readonly ILogger _logger;
        private readonly BraintreeWarpper _braintreeWarpper;
        public TransactionController(ILogger logger, CreditCardService creditCardService, TransactionService transactionService, AbstractValidator<TransactionModel> transactionModelValidator, BraintreeWarpper braintreeWarpper)
        {
            _logger = logger;
            _creditCardService = creditCardService;
            _transactionService = transactionService;
            _transactionModelValidator = transactionModelValidator;
            _braintreeWarpper = braintreeWarpper;
        }
        [HttpPost]
        [Route("sale")]
        public ServiceResponse<bool> Create([FromBody] CreateTransactionModel createModel)
        {
            var response = new ServiceResponse<bool>();

            var creditCardModel = _creditCardService.GetByToken(createModel.Token);
            if (creditCardModel.IsSuccessed)
            {
                TransactionModel model = new TransactionConverter().ConvertCreateModel(createModel);
                decimal amount;
                decimal.TryParse(createModel.Amount, out amount);

                if (_braintreeWarpper.Sale(amount, createModel.Token))
                {
                    model.IsActive = true;
                    model.IsDeleted = false;
                    model.CreditCardId = creditCardModel.Data.ID;

                    var validationResult = _transactionModelValidator.Validate(model);
                    if (validationResult.IsValid)
                    {
                        var entity = new TransactionConverter().Convert(model);
                        var serviceResponse = _transactionService.Create(entity);
                        response.IsSuccessed = serviceResponse.IsSuccessed;
                        response.Errors = serviceResponse.Errors;
                        response.Data = true;
                    }
                    else
                    {
                        _logger.Error("{source} {template} {logtype} {ValidationError}", "controller", "TransactionEntity", "validationerror", validationResult.Errors);
                        response.IsSuccessed = false;
                        response.Errors = new ValidationFailureConverter().Convert(validationResult.Errors.ToList());
                    }
                }
                else
                {
                    response.IsSuccessed = false;
                    response.Errors.Add(new ServiceError() { InnerMessage = "Payment Error.", Message = "There is an error with payment! Please contact.", Code = "2009062057" });
                }
            }
            else
            {
                response.IsSuccessed = false;
                response.Errors.Add(new ServiceError() { InnerMessage = "Token not found in the credit card table.", Message = "There is an error with payment! Please contact.", Code = "2009062304" });
            }

            return response;
        }
        [HttpDelete("guid")]
        public ServiceResponse<bool> Delete(Guid guid) => _transactionService.Delete(guid);
        [HttpGet]
        public ServiceResponse<List<TransactionModel>> List()
        {
            var response = new ServiceResponse<List<TransactionModel>>();
            var transactionConverter = new TransactionConverter();
            var serviceResponse = _transactionService.ListForUser(CurrentUser.ID);
            if (serviceResponse.IsSuccessed) response.Data = transactionConverter.Convert(serviceResponse.Data);
            response.IsSuccessed = serviceResponse.IsSuccessed;
            return response;
        }
    }
}