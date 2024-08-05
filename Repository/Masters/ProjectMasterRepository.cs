using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using Insight.Database;
using Microsoft.Data.SqlClient;

namespace CbtAdminPanel.Repository.Masters
{
    public class ProjectMasterRepository  :IProjectMasterRepository
    {

        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;

        public ProjectMasterRepository(MyDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public ResponseModel AddData(ProjectMaster projectMaster)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                projectMaster.CreatedDate = DateTime.Now;
                projectMaster.CreatedBy = 1;
                _context.Add(projectMaster);
                _context.SaveChanges();
                if (projectMaster.Id > 0)
                {
                    responseModel.Message = "Project Add Successfully";
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

        public async Task<List<ProjectMaster>> ProjectMasterList()
        {
            try
            {
                using (SqlConnection DB = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    #region Search Filter
                    string SearchString = string.Empty;
                    List<ProjectMaster> ModelList = new List<ProjectMaster>();

                    //SearchString = " AND [Transaction].UserId=@USERiD";

                    #endregion
                    #region Query :GetList
                    string CommandText = "  select *,(LM.Locationprefix + LM.LocationId)as LocationName from ProjectMaster as PM left join LocationMaster as LM on LM.Id =PM.LocationId ";

                    #endregion
                    var parameters = new
                    {

                    };

                    ModelList = DB.QuerySql<ProjectMaster>(CommandText, parameters).ToList();
                    return ModelList;
                }
            }
            catch (Exception ex)
            {
                return new List<ProjectMaster>();
            }
        }

        public List<ProjectMaster> AllDataList()
        {
            return _context.ProjectMaster.ToList();
            // return await LocationMasterList();
        }
    }
}
