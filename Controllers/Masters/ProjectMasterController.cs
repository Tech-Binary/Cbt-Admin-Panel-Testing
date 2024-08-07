using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CbtAdminPanel.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ProjectMasterController : BaseController
    {
        private readonly ILocationMasterRepository _Locrepository;
        private readonly IProjectMasterRepository _repository;


        //public ProjectMasterController(IProjectMasterRepository repository, ILocationMasterRepository Locrepository, MyDbcontext _context)
        //{
        //    _repository = repository;
        //    _Locrepository = Locrepository;

        //}
        public ProjectMasterController(IProjectMasterRepository repository, ILocationMasterRepository Locrepository, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context, IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment, contextAccessor, configuration, context)
        {
            _repository = repository;
            _Locrepository = Locrepository;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var data = _Locrepository.AllDataList();

        //    var locations = new List<SelectListItem>(data.Select(us => new SelectListItem { Value = us.Id.ToString(), Text = us.Locationprefix + us.LocationId }));
        //    //  .ToDictionary(us => us.Id, us => us.LocName), "Key", "Value");
        //    ViewBag.Locations = locations;

        //    string Response = "";

        //    if (TempData.ContainsKey("Response"))
        //        Response = TempData["Response"].ToString();
        //    ViewBag.Response = Response;
        //    TempData.Remove("Response");
        //    ViewBag.ProjectMasterList = await _repository.ProjectMasterList();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(ProjectMaster projectMaster)
        //{
        //    ResponseModel response = _repository.AddData(projectMaster);
        //    TempData["Response"] = JsonConvert.SerializeObject(response);
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        [Route("ProjectMasterCreate")]
        public ResponseModel Create(ProjectMaster projectMaster)
        {
            ResponseModel response = _repository.AddData(projectMaster);
            return response;
        }

        [HttpPost]
        [Route("ProjectMasterList")]

        public async Task<List<ProjectMaster>> ProjectMasterList()
        {
            return await _repository.ProjectMasterList();
        }


    }
}
