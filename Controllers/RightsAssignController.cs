using CbtAdminPanel.Interface;
using CbtAdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CbtAdminPanel.Controllers
{

    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class RightsAssignController : BaseController
    {
        private readonly IAssignRightsRepository _Repository;
        public RightsAssignController(IAssignRightsRepository repository, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context, IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment, contextAccessor, configuration, context)
        {
            _Repository = repository;
        }

        [HttpPost]
        [Route("GetprojectusingUserLoaction")]
        public ResponseModel GetprojectusingUserLoaction(int Id)
        {
            var res = _Repository.GetprojectusingUserLoaction(Id);
            return res;
        }

        [HttpPost]
        [Route("GetModuleUsingProject")]
        public ResponseModel GetModuleusingProject(int UserId, int ProjectId)
        {
            var res = _Repository.GetModuleUsingProjectId(UserId, ProjectId);
            return res;
        }

        [HttpPost]
        [Route("ProjectInAddModuleRights")]
        public ResponseModel ProjectInAddModuleRights(List<AssignRights> assigns)
        {
            return _Repository.ProjectInAddModuleRights(assigns);
        }

    }
}
