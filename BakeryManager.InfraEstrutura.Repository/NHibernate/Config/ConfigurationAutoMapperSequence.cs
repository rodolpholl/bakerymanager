using System;
using System.Reflection;
using BakeryManager.InfraEstrutura.Repository.NHibernate.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;

namespace BakeryManager.InfraEstrutura.Repository.NHibernate.Config
{
    

    /// <summary>
    /// Configuração NHibernate para Automapeamento. Configuração padrão da Infra-estrutura BakeryManager.
    /// </summary>
    public class ConfigurationAutoMapperSequence : ConfigurationNHibernate
    {
        
        /// <summary>
        /// Retorna as configurações para serem usadas na seção.
        /// </summary>
        /// <returns>FluentConfiguration</returns>
        public override FluentNHibernate.Cfg.FluentConfiguration GetDefaultConfiguration()
        {
            return base.GetDefaultConfiguration()
                .Mappings(m => m.AutoMappings.Add(AutoMap.Assembly(Assembly.Load(this.Parameters.DomainMap), new AutoMapperConfig(this.Parameters.DomainMap))
                    //Convençõs de Mapeamento e geração da Base de Dados.
                                                      .Conventions.Add(DefaultConventions.Get())

                   )); 
        }
    }
        
    
}
