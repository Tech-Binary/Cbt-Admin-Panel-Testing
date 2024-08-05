using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using CbtAdminPanel.Models.MasterModel.MasterSeries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace CbtAdminPanel.Controllers.Masters
{
    public class LocationMasterController : Controller
    {

        private readonly ILocationSeriesRepository _LocSeriesrepository;
        private readonly ILocationMasterRepository _repository;


        public LocationMasterController(ILocationMasterRepository repository, ILocationSeriesRepository LocSeriesrepository, MyDbcontext _context)
        {
            _repository = repository;
            _LocSeriesrepository = LocSeriesrepository;

        }
        public async Task<IActionResult> Index()
        {
            var data= _LocSeriesrepository.AllDataList();
           
            var locations = new  List<SelectListItem>(data.Select(us => new SelectListItem { Value = us.LocName.ToString(), Text = us.LocName }));
         //  .ToDictionary(us => us.Id, us => us.LocName), "Key", "Value");
            ViewBag.Locations = locations;

            string Response = "";

            if (TempData.ContainsKey("Response"))
                Response = TempData["Response"].ToString();
            ViewBag.Response = Response;
            TempData.Remove("Response");
            var  data1 = _repository.GetCountryList();
            List<SelectListItem> countrylist = new List<SelectListItem>();
            for (int i = 0; i < data1.Rows.Count; i++)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Value = data1.Rows[i].ItemArray[0].ToString();
                listItem.Text = data1.Rows[i].ItemArray[1].ToString();
                countrylist.Add(listItem);
            }
            ViewBag.CountryList = countrylist;
            ViewBag.LocationMasterList =await _repository.LocationMasterList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(LocationMaster locationMaster)
        {
            ResponseModel response = _repository.AddData(locationMaster);
            TempData["Response"] = JsonConvert.SerializeObject(response);
            return RedirectToAction("Index");
        }

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

    }
}
