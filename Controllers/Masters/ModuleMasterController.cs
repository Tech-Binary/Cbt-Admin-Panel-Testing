using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CbtAdminPanel.Controllers.Masters
{
    public class ModuleMasterController : Controller
    {
        private readonly IProjectMasterRepository _projectMasterRepository;
        private readonly IModuleMasterRepository _repository;


        public ModuleMasterController(IModuleMasterRepository repository, IProjectMasterRepository projectMasterRepository, MyDbcontext _context)
        {
            _repository = repository;
            _projectMasterRepository = projectMasterRepository;

        }
        public async Task<IActionResult> Index()
        {
            var data = _projectMasterRepository.AllDataList();

            var project = new List<SelectListItem>(data.Select(us => new SelectListItem { Value = us.Id.ToString(), Text = us.ProjectName }));
            //  .ToDictionary(us => us.Id, us => us.LocName), "Key", "Value");
            ViewBag.Project = project;

            string Response = "";

            if (TempData.ContainsKey("Response"))
                Response = TempData["Response"].ToString();
            ViewBag.Response = Response;
            TempData.Remove("Response");
            ViewBag.ProjectMasterList = await _repository.ModuleMasterList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ModuleMaster moduleMaster)
        {
            ResponseModel response = _repository.AddData(moduleMaster);
            TempData["Response"] = JsonConvert.SerializeObject(response);
            return RedirectToAction("Index");
        }
    }
}
