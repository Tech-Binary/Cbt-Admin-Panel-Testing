using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface;
using CbtAdminPanel.Models;

namespace CbtAdminPanel.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;
        public readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(MyDbcontext context, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public ResponseModel getuserdata(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var user= _context.Users.Where(x=>x.Id==id).FirstOrDefault();
                if (user != null) {
                    response.Data = user;
                    response.Status = StatusEnums.success.ToString();
                        }
                else
                {
                    response.Message = "User Not found";
                    response.Status = StatusEnums.error.ToString();
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = StatusEnums.error.ToString();
            }
            return response;
        }

        public List<Users> GetUserList()
        {
            return _context.Users.ToList();
        }


        public ResponseModel CreateRole(Users user)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
                user.LocationId = "";
                _context.Add(user);
                _context.SaveChanges();
                if (user.Id > 0)
                {
                    responseModel.Message = "User Create Successfully";
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
