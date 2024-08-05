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

        public ModuleMasterRepository(MyDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public ResponseModel AddData(ModuleMaster moduleMaster)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                moduleMaster.CreatedDate = DateTime.Now;
                moduleMaster.CreatedBy = 1;
                moduleMaster.ProjectId = 1;
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
                    string CommandText = "select *,PM.ProjectName from ModuleMaster as MM LEFT JOIN ProjectMaster as PM ON MM.ProjectId = PM.Id";

                    #endregion
                    var parameters = new
                    {

                    };

                    ModelList = DB.QuerySql<ModuleMaster>(CommandText, parameters).ToList();
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
