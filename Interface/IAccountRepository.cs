using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface
{
    public interface IAccountRepository
    {
        ResponseModel CheckUser(UserModel user);
    }
}
