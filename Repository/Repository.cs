using CbtAdminPanel.Interface;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using System.Data;
using System.Linq.Expressions;
using System;
using CbtAdminPanel.Models;
using System.Linq;
using System.Linq.Expressions;
namespace CbtAdminPanel.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MyDbcontext _context;
        public string Create(object tblmas, string Mode, ref Int64 ID, string dbType = "")
        {
            string ErrorMsg = ValidateData(tblmas, Mode);
            string RtnVal = "";
            if (ErrorMsg == "")
            {
                SaveBaseData(ref tblmas, Mode, ref ID);

                if (dbType == "client")
                    RtnVal = SaveClientData();
                else
                    RtnVal = SaveData();
            }
            return RtnVal;
        }
        public string SaveClientData()
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    trans.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return ex.Message;
                }
            }
        }
        public string SaveData()
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    trans.Commit();
                    return "";
                }
                catch (Exception ex)
                {
                   return ex.Message;
                }
            }
        }

        public virtual string ValidateData(object tblmas, string Mode)
        { return ""; }

        public virtual void SaveBaseData(ref object tblmas, string Mode, ref Int64 ID)
        { return; }

        public virtual void Delete(long ID)
        {
            _context.Remove(_context.Set<T>().Find(ID));
            SaveData();
        }

        public IQueryable<T> GetAll(int pageSize, int pageNo = 1)
        {
            return _context.Set<T>().Skip((pageNo - 1) * pageSize).Take(pageSize);
        }


        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            //IQueryable<T> query = _context.Set<T>().Where(predicate);
            //return query;
            IQueryable<T> query = _context.Set<T>().Where(predicate).AsNoTracking();

            return query;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
    }
}
