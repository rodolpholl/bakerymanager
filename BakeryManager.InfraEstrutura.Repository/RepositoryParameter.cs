using System;
using System.Collections.Generic;
using System.Dynamic;

namespace BakeryManager.InfraEstrutura.Repository
{
    /// <summary>
    /// parãmetros de configuração do repositório
    /// </summary>
    public class RepositoryParameter
    {
        /// <summary>
        /// ConnectionString de Conexão Ao banco de Dados
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// nome do Assembly onde estão os domínios da aplicação, para o AUTO-MAPEAMENTO
        /// </summary>
        public string DomainMap { get; set; }

        /// <summary>
        /// nome do Assembly onde estão os domínios da aplicação, para o MAPEAMENTO FLUENT.
        /// </summary>
        public string FluentMap { get; set; }

        /// <summary>
        /// Indica qual ORM será utilizado pelo repositório
        /// </summary>
        public ORM ORMUtilizado { get; set; }


        /// <summary>
        /// Indica o Nome da Sessão default, caso a factory seja do tipo MultiSession
        /// </summary>
        public string DefaultSessionName { get; set; }

        /// <summary>
        /// Indica se a operação irá atualizar o Schema no Banco de Dados (Apenas criará novas tabelas e/ou novos campos. Atualizações, devem ser feitas por um projeto de Migration).
        /// </summary>
        public bool GenerateSchema { get; set; }

        
        /// <summary>
        /// Indica se a auditoria estará ou não ativada para aquele cliente.
        /// </summary>
        public bool AuditoryEnabled { get; set; }

        /// <summary>
        /// Usuário Logado no sistema naquele momento.
        /// </summary>
        public string UsuarioLogado { get; set; }

        /// <summary>
        /// Entidades que não serão logadas. 
        /// </summary>
        public IList<string> SkipTrakingList { get; set; }

       
    }

    public enum ORM
    {
        NHibernate,
        EntityFramework,
        ADO
    }
}
