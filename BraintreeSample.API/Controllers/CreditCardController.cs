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
    [Route("api/creditcard")]
    [AFAuthorization]
    public class CreditCardController : BaseController
    {
        private readonly CreditCardService _creditCardService;
        private readonly AbstractValidator<CreditCardModel> _creditCardModelValidator;
        private readonly BraintreeWarpper _braintreeWarpper;
        private readonly ILogger _logger;
        public CreditCardController(ILogger logger, CreditCardService creditCardService, AbstractValidator<CreditCardModel> creditCardModelValidator, BraintreeWarpper braintreeWarpper)
        {
            _logger = logger;
            _creditCardService = creditCardService;
            _creditCardModelValidator = creditCardModelValidator;
            _braintreeWarpper = braintreeWarpper;
        }
        [HttpPost]
        [Route("tokenize")]
        public ServiceResponse<string> Create([FromBody] CreateCreditCardModel createModel)
        {
            var response = new ServiceResponse<string>();

            int expirationYear;
            Int32.TryParse(createModel.ExpirationYear, out expirationYear);
            int expirationMonth;
            Int32.TryParse(createModel.ExpirationMonth, out expirationMonth);
            int cvv;
            Int32.TryParse(createModel.Cvv, out cvv);

            var model = new CreditCardModel();
            model.ExpirationMonth = expirationMonth;
            model.ExpirationYear = expirationYear;
            model.BinNumber = createModel.CardNumber.Substring(0, Math.Min(createModel.CardNumber.Length, 6));
            model.LastFour = createModel.CardNumber.Substring(createModel.CardNumber.Length - Math.Min(createModel.CardNumber.Length, 4));
            model.UserId = CurrentUser.ID;
            model.IsActive = true;
            model.IsDeleted = false;
            var validationResult = _creditCardModelValidator.Validate(model);

            if (validationResult.IsValid)
            {
                var token = _braintreeWarpper.Tokenize(CurrentUser.FirstName, CurrentUser.LastName, CurrentUser.UserName, createModel.CardNumber, expirationYear, expirationMonth, cvv);
                model.Token = token;

                if (!string.IsNullOrWhiteSpace(token))
                {
                    var entity = new CreditCardConverter().Convert(model);
                    var serviceResponse = _creditCardService.Create(entity);
                    response.IsSuccessed = serviceResponse.IsSuccessed;
                    response.Errors = serviceResponse.Errors;
                    response.Data = model.Token;
                }
                else
                {
                    response.IsSuccessed = false;
                    response.Errors.Add(new ServiceError() { Code = "2009062323", InnerMessage = "Token can not created.", Message = "Credit card can not accepted!" });
                }
            }
            else
            {
                _logger.Error("{source} {template} {logtype} {ValidationError}", "controller", "CreditCardEntity", "validationerror", validationResult.Errors);
                response.IsSuccessed = false;
                response.Errors = new ValidationFailureConverter().Convert(validationResult.Errors.ToList());
            }

            return response;
        }
        [HttpDelete("guid")]
        public ServiceResponse<bool> Delete(Guid guid) => _creditCardService.Delete(guid);
        [HttpGet]
        public ServiceResponse<List<CreditCardModel>> List()
        {
            var response = new ServiceResponse<List<CreditCardModel>>();
            var creditCardConverter = new CreditCardConverter();
            var serviceResponse = _creditCardService.ListForUser(CurrentUser.ID);
            if (serviceResponse.IsSuccessed) response.Data = creditCardConverter.Convert(serviceResponse.Data);
            response.IsSuccessed = serviceResponse.IsSuccessed;
            return response;
        }
    }
}