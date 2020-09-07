using FluentValidation;
using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Converters;
using BraintreeSample.APIHelper.Models;
using BraintreeSample.API.Converters;
using BraintreeSample.API.Models;
using BraintreeSample.API.RequestModels;
using BraintreeSample.API.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using BraintreeSample.API.ResponseModels;

namespace BraintreeSample.API.Controllers
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        private readonly AbstractValidator<UserModel> _userModelValidator;
        private readonly ILogger _logger;
        public UserController(ILogger logger, UserService userService, AbstractValidator<UserModel> userModelValidator)
        {
            _logger = logger;
            _userService = userService;
            _userModelValidator = userModelValidator;
        }
        [HttpPost]
        public ServiceResponse<bool> Create([FromBody] CreateUserModel userLoginModel)
        {
            var response = new ServiceResponse<bool>();
            var model = new UserModel()
            {
                FirstName = userLoginModel.FirstName,
                LastName = userLoginModel.LastName,
                UserName = userLoginModel.UserName,
                Password = userLoginModel.Password,
                Token = new PasswordBuilder().GenerateToken()
            };
            var validationResult = _userModelValidator.Validate(model);
            if (validationResult.IsValid)
            {
                var previouslyCreatedUser = _userService.GetByUserName(model.UserName).Data;
                if (previouslyCreatedUser == null)
                {
                    model.IsActive = true;
                    model.IsDeleted = false;
                    var entity = new UserConverter().Convert(model);
                    var serviceResponse = _userService.Create(entity);
                    response.IsSuccessed = serviceResponse.IsSuccessed;
                    response.Errors = serviceResponse.Errors;
                    response.Data = serviceResponse.IsSuccessed;
                }
                else
                {
                    response.IsSuccessed = false;
                    response.Errors = new List<ServiceError>() { new ServiceError() { Code = "100003", InnerMessage = "Username is already registered.", Message = "Username is already registered." } };
                }
            }
            else
            {
                _logger.Error("{source} {template} {logtype} {ValidationError}", "controller", "UserEntity", "validationerror", validationResult.Errors);
                response.IsSuccessed = false;
                response.Errors = new ValidationFailureConverter().Convert(validationResult.Errors.ToList());
            }
            return response;
        }
        [HttpPost]
        [Route("Login")]
        public ServiceResponse<UserLoginSummaryModel> Login([FromBody] UserLoginModel userLoginModel)
        {
            var response = new ServiceResponse<UserLoginSummaryModel>();
            var model = new UserModel()
            {
                UserName = userLoginModel.UserName,
                Password = (new PasswordBuilder()).Encrpyt(userLoginModel.Password)
            };
            var previouslyCreatedUser = _userService.GetValidUser(model.UserName, model.Password).Data;
            if (previouslyCreatedUser == null)
            {
                response.IsSuccessed = false;
                response.Errors = new List<ServiceError>() { new ServiceError() { Code = "100001", Message = "User credential is not valid.", InnerMessage = "User credential is not valid." }
                };
            }
            else
            {
                var newToken = new PasswordBuilder().GenerateToken();
                previouslyCreatedUser.Token = newToken;
                _userService.Update(previouslyCreatedUser);
                response.IsSuccessed = true;
                var userLoginSummaryModel = new UserLoginSummaryModel();
                userLoginSummaryModel.FirstName = previouslyCreatedUser.FirstName;
                userLoginSummaryModel.LastName = previouslyCreatedUser.LastName;
                userLoginSummaryModel.UserName = previouslyCreatedUser.UserName;
                userLoginSummaryModel.Token = newToken;
                response.Data = userLoginSummaryModel;
            }
            return response;
        }
    }
}