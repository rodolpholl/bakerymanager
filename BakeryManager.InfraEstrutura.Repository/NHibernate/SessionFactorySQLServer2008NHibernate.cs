using BakeryManager.Infraestrutura.Repository.NHibernate.DBFactoryBuilder;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace BakeryManager.Infraestrutura.Repository.NHibernate
{
    /// <summary>
    /// Classe que retorna uma construção MS SQL Server 2008.
    /// </summary>
    public  class SessionFactorySqlServer2008NHibernate : SessionFactoryBase
    {

        public override ISessionFactory GetSessionFactory(bool GenerateSchema)
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
