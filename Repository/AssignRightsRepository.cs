using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface;
using CbtAdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace CbtAdminPanel.Repository
{
    public class AssignRightsRepository : IAssignRightsRepository
    {
        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;
        public readonly IHttpContextAccessor _contextAccessor;

        public AssignRightsRepository(MyDbcontext context, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _contextAccessor = contextAccessor;

        }

        public ResponseModel GetprojectusingUserLoaction(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var User= _context.Users.Where(x=>x.Id==id).FirstOrDefault();
                if (User != null)
                {
                    if (User.LocationId != null || User.LocationId != "")
                    {
                        int[] idArray = User.LocationId.Split(',').Select(int.Parse).ToArray();
                        var res = (from U in _context.ProjectMaster
                                   where idArray.Contains(U.LocationId)
                                   select new
                                   {
                                      Id = U.Id,
                                      projectName=U.ProjectName
                                   }).ToList();
                        responseModel.Status = StatusEnums.success.ToString();
                        responseModel.Data = res;
                    }
                    else
                    {
                        responseModel.Message = "No Location Assign";
                        responseModel.Status = StatusEnums.warning.ToString();
                    }

                }
                else
                {
                    responseModel.Message = "User Not exits";
                    responseModel.Status = StatusEnums.warning.ToString();
                }
            }
            catch(Exception ex) {
                responseModel.Message = ex.Message;
                responseModel.Status = StatusEnums.error.ToString();

            }
            return responseModel;
        }


        public ResponseModel GetModuleUsingProjectId(int UserId, int ProjectId)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var ModuleList = _context.ModuleMaster.Where(x => x.ProjectId == ProjectId).ToList();
                if (ModuleList.Count != 0)
                {

                    foreach (var Module in ModuleList)
                    {
                        var AssignModule = _context.AssignRights.Where(x => x.ProjectId == ProjectId && x.ModuleId == Module.Id && x.UserId == UserId).FirstOrDefault();
                        if (AssignModule != null)
                        {
                            Module.IsView = AssignModule.IsView;
                            Module.IsEdit = AssignModule.IsEdit;
                        }
                    }
                    responseModel.Status = StatusEnums.success.ToString();
                    responseModel.Data = ModuleList;
                }
                else
                {
                    responseModel.Status = StatusEnums.error.ToString();
                    responseModel.Message = "No Module is here";
                }

            }
            catch (Exception ex)
            {
                responseModel.Status = StatusEnums.error.ToString();
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }


        public ResponseModel ProjectInAddModuleRights(List<AssignRights> assigns)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {

                foreach(var a in assigns)
                {
                    var data = _context.AssignRights.Where(x => x.ProjectId == a.ProjectId && x.ModuleId == a.ModuleId && x.UserId == a.UserId).FirstOrDefault();
                    if (data != null)
                    {
                        data.ModifiedDate = DateTime.Now;
                        data.IsEdit = a.IsEdit;
                        data.IsView = a.IsView;
                        data.IsEdit = a.IsEdit;
                        _context.Update(data);
                        _context.SaveChanges();
                    }
                    else
                    {
                        a.ModifiedBy = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
                        a.ModifiedDate = DateTime.Now;
                        _context.Add(a);
                        _context.SaveChanges();
                    }
                    responseModel.Status = StatusEnums.success.ToString();
                    responseModel.Message = "Assign Rights SuccessFully";
                }
            }
            catch(Exception ex)
            {
                responseModel.Status= StatusEnums.error.ToString();
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }
    }
}
