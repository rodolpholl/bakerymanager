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
    internal static class DBFactoryOracle
    {
        internal static ISessionFactory GetOracleSessionFactory(ISessionFactoryNHibernate pSessionFacotyBase)
        {
            try
            {
                ISessionFactory _sessionFactory = null;

                pSessionFacotyBase.Config.PersistenceConfig = OracleDataClientConfiguration
                    .Oracle10
                    .IsolationLevel(System.Data.IsolationLevel.ReadCommitted)
                    
                        #if DEBUG
                        .ShowSql()
                        .UseOuterJoin()
                        .FormatSql()
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
