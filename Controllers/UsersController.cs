using CbtAdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;
using System;
using CbtAdminPanel.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CbtAdminPanel.Controllers
{

    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _repository;
        public UsersController(IUserRepository repository, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context, IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment, contextAccessor, configuration, context)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("GetUser")]
        public ResponseModel GetUserInfo([FromBody] Users res)
        {
            ResponseModel result = _repository.getuserdata(res.Id);
            return result;
        }

        [HttpPost]
        [Route("GetUserList")]
        public List<Users> GetUserList()
        {
            return _repository.GetUserList();
        }

        [HttpPost]
        [Route("CreateUser")]
        public ResponseModel CreateUser([FromBody] Users user)
        {
            ResponseModel res =_repository.CreateUser(user);
            return res;
        }

        [HttpPost]
        [Route("AssignRole")]
        public ResponseModel AssignRole([FromBody] Users user)
        {
            return _repository.AssignLocation(user);
        }

        [HttpGet]
        [Route("AssignLoaction")]
        public ResponseModel AssignLoaction(int id)
        {
            return _repository.UserAssignLoactionList(id);
        }


    }
}
