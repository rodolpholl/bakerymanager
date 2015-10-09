using System.Reflection;
using FluentNHibernate.Cfg;

namespace BakeryManager.Infraestrutura.Repository.NHibernate.Config
{

    /// <summary>
    /// Classe que define o mapeamento com notação Fluent.
    /// </summary>
    public class ConfigurationFluentMapper : ConfigurationNHibernate
    {

        public override FluentConfiguration GetDefaultConfiguration()
        {
            return base.GetDefaultConfiguration()
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load(this.Parameters.FluentMap)));
        }
    }
}
