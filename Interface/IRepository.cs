namespace CbtAdminPanel.Interface
{
    public interface IRepository<T>
    {

        string Create(object tblmas, string Mode, ref Int64 ID, string dbType = "");

        void Delete(long ID);

    }
}
