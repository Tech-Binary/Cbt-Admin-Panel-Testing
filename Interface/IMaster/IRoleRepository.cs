using CbtAdminPanel.Models;

namespace CbtAdminPanel.Interface.IMaster
{
    public interface IRoleRepository
    {
        List<Roles> GetRoles();
        ResponseModel CreateRole(Roles ROLE);
    }
}
