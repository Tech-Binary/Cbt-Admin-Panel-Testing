using CbtAdminPanel.Models.MasterModel;
using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface.IMaster
{
    public interface IProjectMasterRepository
    {
        ResponseModel AddData(ProjectMaster projectMaster);

         Task<List<ProjectMaster>> ProjectMasterList();

        List<ProjectMaster> AllDataList();
    }
}
