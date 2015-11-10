using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using BakeryManager.InfraEstrutura.Repository.Contract;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.Context;
using NHibernate.Transform;
using NHibernate.Util;


namespace BakeryManager.InfraEstrutura.Repository.NHibernate
{
    /// <summary>
    /// Classe repositório genérico para NHibernate
    /// </summary>
    /// <typeparam name="T">Instância do Domínio</typeparam>
    public class RepositoryBaseNHibernate<T> : IRepositoryBase<T> where T : class
    {
        #region "Atributos"


        private ITransaction _transaction;
        private ISession _session;
        internal ISessionFactoryNHibernate SessionFactory { get; set; }
       
        /// <summary>
        /// Seção do NHibernate.
        /// </summary>
        public ISession session
        {
            get
            {
                //if (_session == null || !_session.IsOpen)
                //{
                //    _session = SessionFactory.GetCurrentSession();
                //}

                //return _session.GetSession(EntityMode.Poco);  

                return SessionFactory.GetCurrentSession();
            }
        }

        /// <summary>
        /// Inicia uma nova transação.
        /// </summary>
        /// <returns>ITransaction</returns>
        public ITransaction ObterTransacao()
        {
            if (session == null) throw new ArgumentNullException("session");

            if (!IsOpenTransaction(_transaction))
            {
                _transaction = session.BeginTransaction();
            }

            return _transaction;
        }

        /// <summary>
        /// Cancela a transação
        /// </summary>
        public void RollbackTransaction()
        {
            if (IsOpenTransaction(_transaction))
                _transaction.Rollback();

            _transaction = null;
        }


        private static bool IsOpenTransaction(ITransaction Transaction)
        {
            return Transaction != null && !Transaction.WasCommitted && !Transaction.WasRolledBack;
        }

        #endregion

        #region IBaseRepository<T> Members

        /// <summary>
        /// Retorna uma instência sigular(apenas uma).
        /// </summary>
        /// <param name="primaryKey">Chave Primária no domínio</param>
        /// <returns></returns>
        public T Single(object primaryKey)
        {
            try
            {
                return session.Load<T>(primaryKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Retorna uma instência sigular(apenas uma), ou um nulo.
        /// </summary>
        /// <param name="primaryKey">Chave Primária no domínio</param>
        /// <returns></returns>
        public T SingleOrDefault(object primaryKey)
        {
            return session.Get<T>(primaryKey);
        }

        /// <summary>
        /// Retorna o conjunto de registros existentes para o Domíno
        /// </summary>
        /// <returns>IEnumerable do Domíno</returns>
        public IEnumerable<T> GetAll()
        {
            return session.CreateCriteria<T>().List<T>();
        }

        /// <summary>
        /// Indica se o registro existe na base de dados ou não.
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey)
        {
            return (session.Get<T>(primaryKey) != null);
        }

        /// <summary>
        /// Insere um registro na tabela equivalente ao domínio.
        /// </summary>
        /// <param name="entity">Entidade a ser inclusa</param>
        public virtual T Insert(T entity)
        {
            try
            {
                session.Save(entity);
                session.Flush();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (session.IsOpen)
                    session.Clear();
            }
        }

        /// <summary>
        /// Altera o registro de um determinado domínio na tabela.
        /// </summary>
        /// <param name="entity">Entidade a ser alterada</param>
        public virtual T Update(T entity)
        {
            try
            {
                
                session.Update(entity);
                session.Flush();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (session.IsOpen)
                    session.Clear();
            }
        }

        /// <summary>
        /// Exclui o registro da entidade da tabela
        /// </summary>
        /// <param name="entity">Entidade a ser excluída</param>
        public void Delete(T entity)
        {
            try
            {
                session.Delete(entity);
                session.FlushMode = FlushMode.Auto;
                session.Flush();
            }
            catch (Exception ex)
            {
                session.Clear();
                throw ex;
            }
            finally
            {
                session.Clear();
            }
        }

        /// <summary>
        /// Retorna um conjunto de dados, dada uma determinada propriedade
        /// </summary>
        /// <param name="PropertyName">Nome da Propriedade que será filtrada</param>
        /// <param name="Entity">Valor a ser comparado</param>
        /// <returns> Lista de Objetos do Domínio</returns>
        public IList<T> GetByProperty(string PropertyName, object Entity)
        {
            return session.CreateCriteria<T>().Add(Restrictions.Eq(PropertyName, Entity)).List<T>();
        }

        /// <summary>
        /// Retorna a Quantidade de Registros Dada uma determinada propriedade.
        /// </summary>
        /// <param name="PropertyName">Nome da Propriedade</param>
        /// <param name="Entity">Instância da Entidade Consultada</param>
        /// <returns></returns>
        public long CountByProperty(string PropertyName, T Entity)
        {
            return
                (long)
                session.CreateCriteria<T>().Add(Restrictions.Eq(PropertyName, Entity)).SetProjection(
                    Projections.RowCount()).List()[0];
        }

        public void Dispose()
        {

            SessionFactory.Close();
           
        }

        

        //private T Salvar(T objeto)
        //{
        //    try
        //    {
        //        session.SaveOrUpdate(objeto);
        //        session.Flush();

        //        return objeto;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (session.IsOpen)
        //            session.Clear();
        //    }
        //}

        public IEnumerable<T> Get(IEnumerable<Expression<Func<T, bool>>> Filters,
            IEnumerable<Expression<Func<T, object>>> OrderCriterias,
            IEnumerable<Expression<Func<T, object>>> DescOrderCriterias,
            int PageNumber, int PageSize, out int TotalPages)

        {
            var nhQuery = (IQueryable<T>)session.QueryOver<T>();

            

            IQueryable<T> query = nhQuery;

            if (Filters != null)
                query = Filters.Where(filter => filter != null).Aggregate(query, (current, filter) => current.Where(filter));

            bool pagingEnabled = PageSize > 0;

            if (pagingEnabled)
                TotalPages = (int)Math.Ceiling((decimal)query.Count() / (decimal)PageSize);
            else
                TotalPages = 1;

            if (OrderCriterias != null)
                query = OrderCriterias.Where(orderCriteria => orderCriteria != null).Aggregate(query, (current, orderCriteria) => current.OrderBy(orderCriteria));

            if (DescOrderCriterias != null)
                query = DescOrderCriterias.Where(descOrderCriteria => descOrderCriteria != null).Aggregate(query, (current, descOrderCriteria) => current.OrderByDescending(descOrderCriteria));

            if (pagingEnabled)
                query = query.Skip(PageSize * (PageNumber - 1)).Take(PageSize);

            return query.ToArray();
        }



        public IQueryable<T> Query()
        {
            return session.Query<T>();
        }


        public bool Exists(T entity)
        {
            return session.Query<T>().Contains(entity);
        }

        #endregion

        public void RemoveSession(T entity)
        {
            if (session.Contains(entity))
                session.Evict(entity);
        }


        public void ClearSession()
        {
            _session.Close();
            _session = SessionFactory.GetCurrentSession();
        }


        public T Load(object Id)
        {
            return session.Get<T>(Id);
        }


        public IList SQLQuery(string pSQL)
        {
            //return session.CreateSQLQuery(pSQL).List();
            var query = session.CreateSQLQuery(pSQL);
            return query.SetResultTransformer(Transformers.AliasToEntityMap).List();


        }


        public DataTable SQLQueryDataTable(string pSQL)
        {
            var result = SQLQuery(pSQL);
           
            if (result.Count == 0)
                return null;

            var dtResult = new DataTable("tblResult");

            foreach (var c in ((Hashtable)result[0]).Keys) 
                dtResult.Columns.Add(new DataColumn(c.ToString()));


            foreach (Hashtable hashtable in result)
            {
                var row = dtResult.NewRow();
                foreach (var var in hashtable.Keys)
                    row[var.ToString()] = hashtable[var.ToString()];

                dtResult.Rows.Add(row);
            }

            return dtResult;
        }
    }
}
