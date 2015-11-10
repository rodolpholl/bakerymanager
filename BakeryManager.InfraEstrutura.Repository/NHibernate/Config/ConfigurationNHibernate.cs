using System.Reflection;
using System.Web;
using BakeryManager.InfraEstrutura.Helpers.Security;
using BakeryManager.InfraEstrutura.Repository.NHibernate.Config.Auditory;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using BakeryManager.InfraEstrutura.Repository.Contract;
using NHibernate.Cfg;
using NHibernate.Event;
using BakeryManager.InfraEstrutura.Repository;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Config
{
    /// <summary>
    /// Classe que compõe os parâmetros de configuração para inicialização de uma Seção.
    /// </summary>
    public  class ConfigurationNHibernate : IConfigurationNHibernate
    {

        public virtual RepositoryParameter Parameters { get; set; }
        public virtual IPersistenceConfigurer PersistenceConfig { get; set; }

        public virtual FluentConfiguration GetDefaultConfiguration()
        {

            var nhbConfig = Fluently.Configure()
                .Cache(it => it.Not.UseSecondLevelCache())
                .Database(PersistenceConfig);
            
            nhbConfig.CurrentSessionContext(HttpContext.Current != null ? "web" : "thread_static");

            if (Parameters.AuditoryEnabled)
            {
                nhbConfig.Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<AuditRegister>().Where(x => x.Namespace == typeof(AuditRegister).Namespace && x.Name == typeof(AuditRegister).Name)));
                nhbConfig.ExposeConfiguration(AddAudit);
            }




            return nhbConfig;
        }

        private void AddAudit(Configuration config)
        {
            config.SetListener(ListenerType.PostUpdate, new AuditUpdateEventListener() { Params = this.Parameters });
            config.SetListener(ListenerType.PostInsert, new AuditInsertEventListener() { Params = this.Parameters });
            config.SetListener(ListenerType.PostDelete, new AuditDeleteEventListener() { Params = this.Parameters });
        }

}
}
