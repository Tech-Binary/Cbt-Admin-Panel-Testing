using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;

namespace CbtAdminPanel.Interface.IMaster
{
    public interface IModuleMasterRepository
    {
        ResponseModel AddData(ModuleMaster moduleMaster);

        Task<List<ModuleMaster>> ModuleMasterList();
    }
}
