
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BakeryManager.Infraestrutura.Repository.Contract;



namespace BakeryManager.Infraestrutura.Base.BusinessManagement
{
    public class BusinessManagementBase<T> : IBusinessManagementBase<T>, IDisposable where T : class
    {

        protected IRepositoryBase<T> Repository { get; set; }

        public IList<T> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public void Insert(T Object)
        {
            Repository.Insert(Object);
        }

        public void Update(T Object)
        {
            Repository.Update(Object);
        }

        public T GetByID(long Id)
        {
            return Repository.SingleOrDefault(Id);
        }

        public T GetByID(int Id)
        {
            return Repository.SingleOrDefault(Id);
        }

        public T GetByID(double Id)
        {
            return Repository.SingleOrDefault(Id);
        }

        public void Delete(T Object)
        {
            Repository.Delete(Object);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }

        public bool Exists(T Object)
        {
            return Repository.Exists(Object);
        }

        public void RemoveSession(T Object)
        {
            Repository.RemoveSession(Object);
        }


        public IQueryable<T> Query()
        {
            return Repository.Query();
        }

        public void ClearSession()
        {
            Repository.ClearSession();
        }

        public T Load(object Id)
        {
            return Repository.Load(Id);
        }

        public IList SQLQuery(string pSQL)
        {
            return Repository.SQLQuery(pSQL);
        }


        public DataTable SQLQueryDataTable(string pSQL)
        {
            return Repository.SQLQueryDataTable(pSQL);
        }
    }
}
