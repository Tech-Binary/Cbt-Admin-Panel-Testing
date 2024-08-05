using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface
{
    public interface IUserRepository
    {
        ResponseModel getuserdata(int id);
    }
}
