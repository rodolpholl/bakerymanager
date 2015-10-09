
using System;
using System.Collections;
using NHibernate;

namespace BakeryManager.Infraestrutura.Repository.Contract
{
    internal interface ISessionFactoryNHibernate
    {
     

       /// <summary>
       /// Configurações do NHibernate
       /// </summary>
       IConfigurationNHibernate Config { get; set; }

        ISession GetCurrentSession();

       

       /// <summary>
       /// Retorna uma instância da Session Factory
       /// </summary>
       /// <param name="GenerateSchema">Indica se a operação irá atualizar o Schema no Banco de Dados (Apenas criará novas tabelas e/ou novos campos. Atualizações, devem ser feitas por um projeto de Migration).</param>
       /// <returns>SessionFactory</returns>
       ISessionFactory GetSessionFactory(bool GenerateSchema);

        /// <summary>
        /// Fecha a Sessão Corrente.
        /// </summary>
        void Close();
    }
}
