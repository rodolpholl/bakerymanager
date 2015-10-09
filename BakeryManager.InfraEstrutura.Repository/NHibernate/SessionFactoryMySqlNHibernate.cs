﻿using BakeryManager.Infraestrutura.Repository.NHibernate.DBFactoryBuilder;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace BakeryManager.Infraestrutura.Repository.NHibernate
{
    /// <summary>
    /// Classe que retorna uma construção MySQL
    /// </summary>
    public class SessionFactoryMySqlNHibernate : SessionFactoryBase
    {
       
        public override ISessionFactory GetSessionFactory(bool GenerateSchema)
        {

            try
            {

                return DBFactoryMySQL.GetMySQLSessionFactory(this);

            }
            catch (FluentConfigurationException ex)
            {
                throw ex;
            }
        }

        
    }
}
