using CbtAdminPanel.Models;
using CbtAdminPanel.Models.MasterModel.MasterSeries;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CbtAdminPanel.Interface.IMaster
{
    public interface ILocationSeriesRepository
    {
        ResponseModel AddData(LocationSeries locationSeries);

        List<LocationSeries> AllDataList();
        List<SelectListItem> LocSeriesDropDownList();
    }
}
