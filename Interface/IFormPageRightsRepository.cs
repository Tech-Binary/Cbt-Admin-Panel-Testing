using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface
{
    public interface IFormPageRightsRepository
    {
        ResponseModel AssignFormPageList(int UserId);
    }
}
