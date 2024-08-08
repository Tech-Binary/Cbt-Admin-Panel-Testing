using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using Insight.Database;
using Microsoft.Data.SqlClient;

namespace CbtAdminPanel.Repository.Masters
{
    public class ModuleMasterRepository : IModuleMasterRepository
    {
        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;
        public readonly IHttpContextAccessor _contextAccessor;

        public ModuleMasterRepository(MyDbcontext context, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public ResponseModel AddData(ModuleMaster moduleMaster)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var CHECK= _context.ModuleMaster.Where(m => m.ModuleName == moduleMaster.ModuleName && m.ProjectId ==m.ProjectId).ToList();
                if (CHECK.Count != 0)
                {
                    responseModel.Message = "Module Name already exits";
                    responseModel.Status = StatusEnums.error.ToString();
                }
                moduleMaster.CreatedDate = DateTime.Now;
                moduleMaster.CreatedBy = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
                _context.Add(moduleMaster);
                _context.SaveChanges();
                if (moduleMaster.Id > 0)
                {
                    responseModel.Message = "Module Add Successfully";
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

        public async Task<List<ModuleMaster>> ModuleMasterList()
        {
            try
            {
                using (SqlConnection DB = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    #region Search Filter
                    string SearchString = string.Empty;
                    List<ModuleMaster> ModelList = new List<ModuleMaster>();

                    //SearchString = " AND [Transaction].UserId=@USERiD";

                    #endregion
                    #region Query :GetList
                    string CommandText = "select *,PM.ProjectName as ProjectName,US.UserName As UserName  from ModuleMaster as MM LEFT JOIN ProjectMaster as PM ON MM.ProjectId = PM.Id left join Users as US on US.Id = MM.CreatedBy";

                    #endregion
                    var parameters = new
                    {

                    };

                    ModelList = DB.QuerySql<ModuleMaster>(CommandText, parameters).ToList() ;
                    return ModelList;
                }
            }
            catch (Exception ex)
            {
                return new List<ModuleMaster>();
            }
        }
        
    }
}
