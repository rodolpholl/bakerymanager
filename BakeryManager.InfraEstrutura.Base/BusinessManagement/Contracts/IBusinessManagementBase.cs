using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BakeryManager.InfraEstrutura.Repository.Contract;

namespace BakeryManager.InfraEstrutura.Base.BusinessManagement
{
    public interface IBusinessManagementBase<T> where T : class
    {
        IList<T> GetAll();
        void Insert(T Object);
        void Update(T Object);
        T GetByID(long Id);
        T GetByID(int Id);
        T GetByID(double Id);
        bool Exists(T Object);
        void Delete(T Object);
        void Dispose();
        void RemoveSession(T Object);
        IQueryable<T> Query();
        void ClearSession();
        T Load(object id);
        IList SQLQuery(string pSQL);
        DataTable SQLQueryDataTable(string pSQL);

    }
}
