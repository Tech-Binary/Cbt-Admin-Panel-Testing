using CbtAdminPanel.Interface;
using CbtAdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CbtAdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class FormPagesRightsController : BaseController
    {
        private readonly IFormPageRightsRepository _repository;
        public FormPagesRightsController(IFormPageRightsRepository repository, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context, IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment, contextAccessor, configuration, context)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetFormRightsList")]
        public ResponseModel GetFormRightsList(int id)
        {
            return _repository.AssignFormPageList(id);
        }
    }
}
