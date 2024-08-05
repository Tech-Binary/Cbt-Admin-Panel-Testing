using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface;
using CbtAdminPanel.Models;

namespace CbtAdminPanel.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;

        public UserRepository(MyDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

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
    }
}
