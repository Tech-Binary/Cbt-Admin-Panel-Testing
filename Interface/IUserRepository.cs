using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface
{
    public interface IUserRepository
    {
        ResponseModel getuserdata(int id);

        List<Users> GetUserList();

        ResponseModel CreateUser(Users user);
    }
}
