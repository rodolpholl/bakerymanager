using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.InfraEstrutura.Repository.Contract;
using NHibernate;
using NHibernate.Context;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.MultiSession
{
    public class MultiSessionFactoryBase : ISessionFactoryNHibernate
    {


        public MultiSessionFactoryBase()
        {
            SessionFactoryList = new Dictionary<string,object>();
        }
        
        public bool GenerateSchema { get; set; }

        public IConfigurationNHibernate Config { get; set; }

        /// <summary>
        /// Lista de SessionFactorys Disponíveis.
        /// </summary>
        public IDictionary SessionFactoryList { get; set; }

        public ISession GetSessionByName(string pSessionName)
        {
            ISession _session = null;

            if (SessionFactoryList[pSessionName] == null)
            {
                var sf = GetSessionFactory(GenerateSchema);
                SessionFactoryList.Add(pSessionName, sf);
                _session = ((ISessionFactory)SessionFactoryList[pSessionName]).OpenSession();

                if (CurrentSessionContext.HasBind((ISessionFactory)SessionFactoryList[pSessionName]))
                    CurrentSessionContext.Unbind((ISessionFactory)SessionFactoryList[pSessionName]);

                CurrentSessionContext.Bind(_session);

            }
            else
                //if (!CurrentSessionContext.HasBind((ISessionFactory)SessionFactoryList[pSessionName]) ||
                //    ((ISessionFactory)SessionFactoryList[pSessionName]).GetCurrentSession() != null)
                //    _session = ((ISessionFactory) SessionFactoryList[pSessionName]).GetCurrentSession();
                //else
            {
                if (((ISessionFactory) SessionFactoryList[pSessionName]).GetCurrentSession() == null)
                {
                    _session = ((ISessionFactory)SessionFactoryList[pSessionName]).OpenSession();
                    if (CurrentSessionContext.HasBind((ISessionFactory)SessionFactoryList[pSessionName]))
                        CurrentSessionContext.Unbind((ISessionFactory)SessionFactoryList[pSessionName]);

                    CurrentSessionContext.Bind(_session);
                }
                    
                else
                    _session = ((ISessionFactory) SessionFactoryList[pSessionName]).GetCurrentSession();

                    
                }


            return _session;
                

        }

        public virtual ISessionFactory GetSessionFactory(bool GenerateSchema)
        {
            return null;
        }


        public ISession GetCurrentSession()
        {
            return GetSessionByName(Config.Parameters.DefaultSessionName);
        }


        public void Close(string pSessionName)
        {
            if ((ISessionFactory)SessionFactoryList[pSessionName] != null)
            {
                if (CurrentSessionContext.HasBind((ISessionFactory)SessionFactoryList[pSessionName]) )
                {

                    if (((ISessionFactory) SessionFactoryList[pSessionName]).GetCurrentSession().IsOpen)
                        ((ISessionFactory) SessionFactoryList[pSessionName]).GetCurrentSession().Close();

                }


            }
        }


        public void Close()
        {
            Close(Config.Parameters.DefaultSessionName);
        }
    }
}
