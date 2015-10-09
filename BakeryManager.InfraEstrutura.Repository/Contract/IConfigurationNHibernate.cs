using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using BakeryManager.InfraEstrutura.Repository;

namespace BakeryManager.Infraestrutura.Repository.Contract
{
    /// <summary>
    /// Interface padrão para todos os arquivos de configuration, que poderão ser utilizados pelo NHibernate
    /// </summary>
    public interface IConfigurationNHibernate
    {

        /// <summary>
        /// Parâmetro do Classe, que são definidos no Container IoC.
        /// </summary>
        RepositoryParameter Parameters { get; set; }

        /// <summary>
        /// Configuração do tipo de persistência a ser executada (MS SQL Server 2008, Oracle, MySQL etc.).
        /// </summary>
        IPersistenceConfigurer PersistenceConfig { get; set; }

        /// <summary>
        /// Assinatura de método para retorno das configurações iniciais do NHibernate
        /// </summary>
        /// <returns>FluentConfiguration</returns>
        FluentConfiguration GetDefaultConfiguration();
    }

}
