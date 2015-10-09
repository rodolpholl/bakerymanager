using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace BakeryManager.Infraestrutura.Repository.Contract
{
    public interface IRepositoryBase<T> : IDisposable
    {

        /// <summary>
        /// Retorna uma instância única usando sua primary key. Caso não encontre, retornará uma exceção.
        /// </summary>
        /// <param name="primaryKey">Primary key do Registro</param>
        /// <returns>T</returns>
        T Single(object primaryKey);

        /// <summary>
        /// Retorna uma instância única do item pela sua primary key. Caso não encontre, retornará null.
        /// Retrieve a single item by it's primary key or return null if not found
        /// </summary>
        /// <param name="primaryKey">Prmary key para pesquisa</param>
        /// <returns>T</returns>
        T SingleOrDefault(object primaryKey);

        /// <summary>
        /// Retorna todas as linhas para a classe T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Verifica se a Primary Key informada já existe para a classe T
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        bool Exists(object primaryKey);

        /// <summary>
        /// Insere o registro na tabela
        /// </summary>
        /// <param name="entity">Entidade a ser inserida</param>
        /// <param name="userId">Usuário que Fará a Inclusão</param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// Atualiza as informações da entidade na base de dados usando sua primary key
        /// Updates this entity in the database using it's primary key
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        /// <param name="userId">The user performing the update</param>
        T Update(T entity);


        /// <summary>
        /// Exclui a entidade da base de dados
        /// ** ATENÇÃO - Alguns itens podem ser marcados como inativos e atualizados, não excluídos (exclusão lógica).
        /// </summary>
        /// <param name="entity">Entidade a ser excluída</param>
        /// <param name="userId">ID do Usuário responsável pela exclusão.</param>
        void Delete(T entity);

        /// <summary>
        /// Retorna a Quantidade de Registros Dada uma determinada propriedade.
        /// </summary>
        /// <param name="PropertyName">Nome da Propriedade</param>
        /// <param name="Entity">Instância da Entidade Consultada</param>
        /// <returns></returns>
        long CountByProperty(string PropertyName, T Entity);

        /// <summary>
        /// Unit of work Padrão.
        /// </summary>
        //IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///Retorna uma lista de valores, onde o valor da propriedade informada for exatamente igual ao parâmetro.
        /// </summary>
        /// <param name="PropertyName">Nome da Propriedade.</param>
        /// <param name="Entity">Valor para comparação.</param>
        /// <returns></returns>
        IList<T> GetByProperty(string PropertyName, object Entity);


        /// <summary>
        /// Retorna um conjunto de dados, baseado nos parâmetos de Consulta informados
        /// </summary>
        /// <param name="Filters"> Função Linq para o filtro a ser utilizado</param>
        /// <param name="OrderCriterias">Função link para Relacionar a ordem do retorno</param>
        /// <param name="DescOrderCriterias">Função link para Relacionar a ordem decrescente do retorno </param>
        /// <param name="PageNumber">Número da Página a ser retornada, em caso de paginação.</param>
        /// <param name="PageSize">Número de linhas que a página terá</param>
        /// <param name="TotalPages">Retorna o total de Páginas da consulta</param>
        /// <returns>Lista de Objetigos do Tipo definido</returns>
        IEnumerable<T> Get(IEnumerable<Expression<Func<T, bool>>> Filters,
                           IEnumerable<Expression<Func<T, object>>> OrderCriterias,
                           IEnumerable<Expression<Func<T, object>>> DescOrderCriterias,
                           int PageNumber, int PageSize, out int TotalPages);

        
        /// <summary>
        /// Retorna um objeto IQueriable para ser verificado.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();

        /// <summary>
        /// Verifica se o usuário existe no banco de dados
        /// </summary>
        /// <param name="entity">Entidade a ser excluída verificada</param>
        /// <returns>True = Usuário Encontrado</returns>
        bool Exists(T entity);

        void RemoveSession(T Object);

        /// <summary>
        /// Executa um comando SQL, Retornando uma lista de objetos genéricos.
        /// </summary>
        /// <param name="pSQL">Query a Ser Executada</param>
        /// <returns>Lista de Resultados no objeto genérico</returns>
        IList SQLQuery(string pSQL);

        /// <summary>
        /// Executa um comando SQL, Retornando um DataTable
        /// </summary>
        /// <param name="pSQL">Query a Ser Executad</param>
        /// <returns>Retorna um DataTable com o resultado</returns>
        DataTable SQLQueryDataTable(string pSQL);

        void ClearSession();

        T Load(object Id);




    }
}
