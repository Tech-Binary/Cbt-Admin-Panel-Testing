using CbtAdminPanel.Interface;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CbtAdminPanel.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class RoleController : BaseController
    {

        private readonly IRoleRepository _repository;
        public RoleController(IRoleRepository repository, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context, IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment, contextAccessor, configuration, context)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("RoleList")]
        public List<Roles> RoleList()
        {
            return _repository.GetRoles();
        }

        [HttpPost]
        [Route("RoleAdd")]
        public ResponseModel RoleAdd([FromBody] Roles roles)
        {
            ResponseModel res = _repository.CreateRole(roles);
            return res;
        }
    }
}
