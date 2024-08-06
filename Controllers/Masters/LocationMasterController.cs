using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Migrations;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using CbtAdminPanel.Models.MasterModel.MasterSeries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace CbtAdminPanel.Controllers.Masters
{

    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class LocationMasterController : BaseController
    {

        private readonly ILocationSeriesRepository _LocSeriesrepository;
        private readonly ILocationMasterRepository _repository;


        public LocationMasterController(ILocationMasterRepository repository, ILocationSeriesRepository LocSeriesrepository, IHttpContextAccessor contextAccessor, IConfiguration configuration, MyDbcontext context, IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment, contextAccessor, configuration, context)
        {
            _repository = repository;
            _LocSeriesrepository = LocSeriesrepository;
        }

        [HttpPost]
        [Route("GetcityList")]
        public List<SelectListItem> GetcityList(int Id)
        {
            var data1 = _repository.GetCityList(Id);
            List<SelectListItem> CityList = new List<SelectListItem>();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = data1.Rows[i].ItemArray[0].ToString();
                listItem.Text = data1.Rows[i].ItemArray[1].ToString();
                CityList.Add(listItem);
            }
            return CityList;
        }

        [HttpPost]
        [Route("LocationMasterList")]
        public async Task<List<LocationMaster>> LocationMasterList()
        {
            var res= await _repository.LocationMasterList();
            return res;
        }

        [HttpPost]
        [Route("LocationMasterCreate")]
        public ResponseModel LocationMasterCreate(LocationMaster locationMaster)
        {
            ResponseModel response = _repository.AddData(locationMaster);
            return response;
        }


        [HttpPost]
        [Route("GetCountryList")]
        public List<SelectListItem> GetCountryList()
        {
            var data1 = _repository.GetCountryList();
            List<SelectListItem> countrylist = new List<SelectListItem>();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = data1.Rows[i].ItemArray[0].ToString();
                listItem.Text = data1.Rows[i].ItemArray[1].ToString();
                countrylist.Add(listItem);
            }
            return countrylist;
        }



    }
}
