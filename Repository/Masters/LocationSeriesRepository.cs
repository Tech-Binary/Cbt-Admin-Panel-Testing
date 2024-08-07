﻿using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Migrations;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using CbtAdminPanel.Models.MasterModel.MasterSeries;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CbtAdminPanel.Repository.Masters
{
    public class LocationSeriesRepository : ILocationSeriesRepository
    {
        private readonly MyDbcontext _context;
        public readonly IHttpContextAccessor _contextAccessor;

        public LocationSeriesRepository(MyDbcontext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public ResponseModel AddData(LocationSeries locationSeries)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var validate = _context.LocationSeries.Where(x => x.LocName == locationSeries.LocName).ToList();
                if (validate.Count > 0)
                {
                    responseModel.Message = "Location Name ALready Exits";
                    responseModel.Status = StatusEnums.error.ToString();
                    return responseModel;
                }
                locationSeries.CreatedDate = DateTime.Now;
                locationSeries.Createdby = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
                locationSeries.Active = true;
                _context.Add(locationSeries);
                _context.SaveChanges();
                if (locationSeries.Id > 0)
                {
                    responseModel.Message = "Location Series Add Successfully";
                    responseModel.Status = StatusEnums.success.ToString();
                }
                else
                {
                    responseModel.Message = "Something went Wrong";
                    responseModel.Status = StatusEnums.error.ToString();
                }

            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Status = StatusEnums.error.ToString();
            }
            return responseModel;
        }

        public List<LocationSeries> AllDataList()
        {
            var roles = (from LocationSeries in _context.LocationSeries
                         join user in _context.Users on LocationSeries.Createdby equals user.Id
                         select (new LocationSeries
                         {
                             Id = LocationSeries.Id,
                             LocName = LocationSeries.LocName,
                             Createdby = LocationSeries.Createdby,
                             CreatedDate = LocationSeries.CreatedDate,
                             UserName = user.UserName
                         })).ToList();
            return roles;
        }

        public List<SelectListItem> LocSeriesDropDownList()
        {
            List<SelectListItem> result = (from p in _context.LocationSeries
                                           orderby p.LocName
                                           select new SelectListItem
                                           {
                                               Text = p.LocName,
                                               Value = p.Id.ToString()
                                           }).Distinct().ToList();
            return result;
        }

    }
}
