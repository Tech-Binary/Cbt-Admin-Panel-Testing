using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface
{
    public interface IAssignRightsRepository
    {

        ResponseModel GetprojectusingUserLoaction(int id);
        ResponseModel GetModuleUsingProjectId(int UserId, int ProjectId);
        ResponseModel ProjectInAddModuleRights(List<AssignRights> assigns);
    }
}
