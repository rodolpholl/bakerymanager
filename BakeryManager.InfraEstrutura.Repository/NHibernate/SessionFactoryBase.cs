using System;
using System.Collections;
using BakeryManager.InfraEstrutura.Repository.Contract;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate
{
    /// <summary>
    /// Classe construtora de uma Seção do NHibernate
    /// </summary>
    public class SessionFactoryBase : ISessionFactoryNHibernate
    {
        
       

        protected static ISessionFactory _sessionFactory = null;

        /// <summary>
        /// Configuração do NHibernate
        /// </summary>
        public IConfigurationNHibernate Config { get; set; }


        /// <summary>
        /// Retorna a Seção Corrente
        /// </summary>
        /// <returns>Retorna um ISession</returns>
        public virtual ISession GetCurrentSession()
        {
            if (_sessionFactory == null )
            {
                _sessionFactory = GetSessionFactory(Config.Parameters.GenerateSchema);
            }

            try
            {
                if (CurrentSessionContext.HasBind(_sessionFactory))
                {
                    if (_sessionFactory.GetCurrentSession().IsOpen)
                        return _sessionFactory.GetCurrentSession();
                    else
                        _sessionFactory.OpenSession();

                }
            }
            catch (Exception ex)
            {
                if ((!ex.GetType().Name.Contains("HibernateException")) || !(ex.Message.Contains("No current session context configured.")))
                {
                    throw ex;
                }
            }

            ISession session = _sessionFactory.OpenSession();

            CurrentSessionContext.Bind(session);

            return session;
        }

        /// <summary>
        /// retorna uma nova seção do NHibernate
        /// </summary>
        /// <param name="GenerateSchema">indica se o schema será atualizado ou não.</param>
        /// <returns>objeto SessionFactory</returns>
        public virtual ISessionFactory GetSessionFactory(bool GenerateSchema)
        {
            return null;
        }

        public virtual void Close()
        {
            if (!_sessionFactory.IsClosed)
            {
                GetCurrentSession().Close();
                _sessionFactory.Close();

            }

        }









    }
}
