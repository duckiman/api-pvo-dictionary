using System;
using System.Linq;
using System.Linq.Expressions;

namespace pvo_dictionary_api.Respositories
{
    interface IRespositoryBase<T>
    {

        /// <summary>
        ///     Retrieve all data of repository
        /// </summary>
        /// <returns>IQueryable<T></returns>
        IQueryable<T> FindAll();

        /// <summary>
        ///     Retrieve all data of repository by Condition
        /// </summary>
        /// <param name="expression">Condition</param>
        /// <returns>IQueryable<T></returns>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        ///     Find data by id
        /// </summary>
        /// <param name="id">Table's primary key</param>
        /// <returns>Object</returns>
        T FindOrFail(object id);

        /// <summary>
        ///     Save a new entity in repository
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Object</returns>
        T Create(T entity);

        /// <summary>
        ///     Update a entity in repository by entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>T</returns>
        T UpdateByEntity(T entity);

        /// <summary>
        ///    Delete a entity in repository by entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Bool</returns>
        bool DeleteByEntity(T entity);
    }
}
