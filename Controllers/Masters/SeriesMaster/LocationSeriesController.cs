using CbtAdminPanel.Interface;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel.MasterSeries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace CbtAdminPanel.Controllers.Masters.SeriesMaster
{
    public class LocationSeriesController : BaseController
    {
        private readonly ILocationSeriesRepository _repository;

        public LocationSeriesController(ILocationSeriesRepository repository, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context, IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment, contextAccessor, configuration, context)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            string Response="";

            if (TempData.ContainsKey("Response"))
                Response = TempData["Response"].ToString();
            ViewBag.Response = Response;
            ViewBag.LocationSeriesList = _repository.AllDataList();
            TempData.Remove("Response");
            return View();
        }

        [HttpPost]
        public IActionResult Create(LocationSeries locationSeries) {
            ResponseModel response= _repository.AddData(locationSeries);
            TempData["Response"]=JsonConvert.SerializeObject(response);
            return RedirectToAction("Index");
        }


        [Route("LocationSeriesList")]
        [HttpPost]
        public List<LocationSeries> AllDataList()
        {
            return _repository.AllDataList();
        }

        [Route("LocationSeriesCreate")]
        [HttpPost]
        public ResponseModel LocationSeriesCreate(LocationSeries locationSeries)
        {
            ResponseModel response = _repository.AddData(locationSeries);
            return response;
        }
    }
}
