using CbtAdminPanel.Constant;
using CbtAdminPanel.Interface;
using CbtAdminPanel.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CbtAdminPanel.Repository
{
    public class FormPagesRightsRepository : IFormPageRightsRepository
    {

        private readonly MyDbcontext _context;
        public readonly IConfiguration _configuration;

        public FormPagesRightsRepository(MyDbcontext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ResponseModel AssignFormPageList(int UserId)
        {
           ResponseModel model = new ResponseModel();
            try
            {
                var user=_context.Users.Where(x=>x.Id== UserId).FirstOrDefault();
                if (user == null)
                {
                    model.Status = StatusEnums.error.ToString();
                    model.Message = "User Not Found";
                }
                else
                {
                    if (user.Role == 1)
                    {
                        var Data = (from u in _context.FormPages
                                    select new
                                    {
                                        FormId = u.FormId,
                                        FormName=u.Name
                                    }).ToList();
                        model.Status = StatusEnums.success.ToString();
                        model.Data = Data;
                    }
                    else
                    {
                       if(user.PageRights == "" || user.PageRights ==null)
                        {
                            model.Status = StatusEnums.warning.ToString();
                            model.Message ="You Dont Have Rights" ;
                        }
                        else
                        {
                            int[] idArray = user.PageRights.Split(',').Select(int.Parse).ToArray();
                            var res = (from U in _context.FormPages
                                       where idArray.Contains(U.FormId)
                                       select new
                                       {
                                           FormId = U.FormId,
                                           FormName = U.Name
                                       }).ToList();
                            model.Status = StatusEnums.error.ToString();
                            model.Data = res;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
                model.Status = StatusEnums.error.ToString();
            }
            return model;
        }
    }
}
