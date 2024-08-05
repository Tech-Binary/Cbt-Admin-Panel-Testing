using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel;
using System.Data;

namespace CbtAdminPanel.Interface.IMaster
{
    public interface ILocationMasterRepository
    {
        ResponseModel AddData(LocationMaster locationMaster);
        List<LocationMaster> AllDataList();
        DataTable GetCityList(int CountryId);
        DataTable GetCountryList();
         Task<List<LocationMaster>> LocationMasterList();
    }
}
