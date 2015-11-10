using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Repository.NHibernate.DBFactoryBuilder;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate
{
    /// <summary>
    /// Classe que retorna uma construção Oracle.
    /// </summary>
    public class SessionFactoryOracleNHibernate : SessionFactoryBase
    {
        public override ISessionFactory GetSessionFactory(bool GenerateSchema)
        {
            
            try
            {

                return DBFactoryOracle.GetOracleSessionFactory(this);

            }
            catch (FluentConfigurationException ex)
            {
                throw ex;
            }

           
        }
    }
}
