using BraintreeSample.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BraintreeSample.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public UserEntity CurrentUser { get; set; }
        public BaseController()
        {
            CurrentUser = new UserEntity();
        }
    }
}