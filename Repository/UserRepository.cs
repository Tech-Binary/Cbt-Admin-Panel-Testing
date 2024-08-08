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
            var users = (from user in _context.Users
                         join role in _context.Roles on user.Role equals role.Id
                         select (new Users
                         {
                             Id = user.Id,
                             FullName = user.FullName,
                             UserName=user.UserName,
                             Email=user.Email,
                             RoleName = role.Name,
                             Role=user.Role,
                             AccountStatus=user.AccountStatus
                         })).ToList();
            return users;
        }


        public ResponseModel CreateUser(Users user)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var usercheck=_context.Users.Where(x=>x.UserName==user.UserName).FirstOrDefault();
                if (usercheck != null)
                {
                    responseModel.Message = "UserName already here";
                    responseModel.Status = StatusEnums.warning.ToString();
                    return responseModel;
                }
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("UserID"));
                user.AccountStatus =user.AccountStatus;
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

        public ResponseModel AssignLocation(Users user)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var usercheck = _context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                if (usercheck == null)
                {
                    responseModel.Message = "UserName Not exits here";
                    responseModel.Status = StatusEnums.warning.ToString();
                    return responseModel;
                }
                usercheck.LocationId = user.LocationId;
                _context.Update(usercheck);
                _context.SaveChanges();
                responseModel.Message = "Location Assign Successfully";
                responseModel.Status = StatusEnums.success.ToString();
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Status = StatusEnums.error.ToString();
            }
            return responseModel;
        }

        public ResponseModel UserAssignLoactionList(int UserId)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {

                var user = _context.Users.Where(x => x.Id == UserId).FirstOrDefault();
                if (user.LocationId != null || user.LocationId != "")
                {
                    int[] idArray = user.LocationId.Split(',').Select(int.Parse).ToArray();
                    var res = (from U in _context.LocationMaster
                               where idArray.Contains(U.Id)
                               select new
                               {
                                   Locationname = U.LocationName,
                                   LoactionId = U.Locationprefix + U.LocationId,
                                   EmpolyeName = user.FullName
                               }).ToList();
                    responseModel.Status= StatusEnums.success.ToString();
                    responseModel.Data = res;
                }
                else
                {
                    responseModel.Message = "No Location Assign";
                    responseModel.Status = StatusEnums.warning.ToString();
                }
            }
            catch(Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Status= StatusEnums.error.ToString();
            }
            return responseModel;
        }
    }
}
