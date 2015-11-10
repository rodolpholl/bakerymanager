using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Repository.Contract;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.DBFactoryBuilder
{
    internal class DBFactorySQLServer2008
    {
        internal static ISessionFactory GetSQLServer2008SessionFactory(ISessionFactoryNHibernate pSessionFacotyBase)
        {
            try
            {
                ISessionFactory _sessionFactory = null;

                pSessionFacotyBase.Config.PersistenceConfig = MsSqlConfiguration
                    .MsSql2008
                    .IsolationLevel(System.Data.IsolationLevel.ReadCommitted)

                    #if DEBUG
                    //.UseOuterJoin()
                    .FormatSql()
                    .ShowSql()
       
                    #endif

                    .ConnectionString(pSessionFacotyBase.Config.Parameters.ConnectionString);

                var nhbConfig = pSessionFacotyBase.Config.GetDefaultConfiguration();

                if (pSessionFacotyBase.Config.Parameters.GenerateSchema)
                    _sessionFactory = nhbConfig.ExposeConfiguration(
                        c => new SchemaUpdate(c).Execute(true, true))
                        .BuildSessionFactory();
                else
                    _sessionFactory = nhbConfig.BuildSessionFactory();


                return _sessionFactory;
            }
            catch (FluentConfigurationException ex)
            {
                throw ex;
            }
        }
    }
}
