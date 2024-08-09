using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface
{
    public interface IUserRepository
    {
        ResponseModel getuserdata(int id);

        List<Users> GetUserList();

        ResponseModel CreateUser(Users user);

        ResponseModel AssignLocation(Users user);

        ResponseModel UserAssignLoactionList(int UserId);

    }
}
