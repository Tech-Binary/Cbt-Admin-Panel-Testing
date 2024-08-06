using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface.IMaster;
using CbtAdminPanel.Models;

namespace CbtAdminPanel.Repository.Masters
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;
        public readonly IHttpContextAccessor _contextAccessor;

        public RoleRepository(MyDbcontext context, IConfiguration configuration,IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public List<Roles> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public ResponseModel CreateRole(Roles ROLE)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                ROLE.CreatedDate = DateTime.Now;
                ROLE.CreatedBy =Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
                _context.Add(ROLE);
                _context.SaveChanges();
                if (ROLE.Id > 0)
                {
                    responseModel.Message = "Role Add Successfully";
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

    }
}
