using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using CbtAdminPanel.Models.MasterModel.MasterSeries;
using Insight.Database;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
//using Dapper;
using System.Data.Common;
using System.Security.AccessControl;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CbtAdminPanel.Repository.Masters
{
    public class LocationMasterRepository : ILocationMasterRepository
    {
        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;

        public LocationMasterRepository(MyDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public ResponseModel AddData(LocationMaster locationMaster)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {

                locationMaster.CreatedDate = DateTime.Now;
                locationMaster.CreatedBy = 1;
                locationMaster.State = 1;
                //locationMaster.City = 1;
                _context.Add(locationMaster);
                _context.SaveChanges();
                if (locationMaster.Id > 0)
                {
                    responseModel.Message = "Location Add Successfully";
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

        public List<LocationMaster> AllDataList()
        {
           return _context.LocationMaster.ToList();
          // return await LocationMasterList();
        }

        public DataTable GetCityList(int CountryId)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                    new SqlParameter("@CountryId",CountryId)
                    };
                return GetDataTable("CityList", para);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }
        public DataTable GetCountryList()
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                    };
                return GetDataTable("CountryList", para);
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
        }
        public DataTable GetDataTable(String SPName, SqlParameter[] para)
        {
            var connectionStr = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = SPName;
                foreach (SqlParameter par in para)
                {
                    cmd.Parameters.Add(par);
                }
                cmd.Connection = con;
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }

        public async Task<List<LocationMaster>> LocationMasterList()
        {
            try
            {
                using (SqlConnection DB = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    #region Search Filter
                    string SearchString = string.Empty;
                    List<LocationMaster> ModelList = new List<LocationMaster>();

                    //SearchString = " AND [Transaction].UserId=@USERiD";

                    #endregion
                    #region Query :GetList
                    string CommandText = "  select *,CM.Name as CityName,CN.Name as CountryName from LocationMaster as LM LEFT join TP_CityMaster AS CM ON LM.City=CM.Id left join TP_CountryMaster as CN ON CN.Id = LM.Country";

                    #endregion
                    var parameters = new
                    {

                    };

                    ModelList = DB.QuerySql<LocationMaster>(CommandText, parameters).ToList();
                    return ModelList;
                }
            }
            catch (Exception ex)
            {
                return new List<LocationMaster>();
            }
        }

    }
}