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
            var roles = (from role in _context.Roles
                         join user in _context.Users on role.CreatedBy equals user.Id
                         select (new Roles
                         {
                             Id = role.Id,
                             Name = role.Name,
                             CreatedBy = role.CreatedBy,
                             CreatedDate = role.CreatedDate,
                             UserName = user.UserName
                         })).ToList();
            return roles;
        }

        public ResponseModel CreateRole(Roles ROLE)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var check=_context.Roles.Where(r => r.Name == ROLE.Name).FirstOrDefault();
                if (check != null)
                {
                    responseModel.Message = "Role already exists";
                    responseModel.Status = StatusEnums.warning.ToString();
                    return responseModel;
                }
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
