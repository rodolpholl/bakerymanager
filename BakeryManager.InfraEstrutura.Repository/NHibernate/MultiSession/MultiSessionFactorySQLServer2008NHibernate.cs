using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Infraestrutura.Repository.NHibernate.DBFactoryBuilder;
using FluentNHibernate.Cfg;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.MultiSession
{
    public class MultiSessionFactorySQLServer2008NHibernate : MultiSessionFactoryBase
    {
        public override global::NHibernate.ISessionFactory GetSessionFactory(bool GenerateSchema)
        {
            try
            {
                return DBFactorySQLServer2008.GetSQLServer2008SessionFactory(this);

            }
            catch (FluentConfigurationException ex)
            {
                throw ex;
            }
        }
    }
}
