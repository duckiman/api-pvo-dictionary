using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using pvo_dictionary_api.Database;
using pvo_dictionary_api.Common;
using EFCore.BulkExtensions;

namespace pvo_dictionary_api.Respositories
{
    public class BaseRespository<T> : IRespositoryBase<T> where T : class, new()
    {
        protected DbSet<T> Model { get; set; }
        protected DatabaseContext DbContext { get; set; }
        protected ApiOption ApiConfig { get; set; }
        public BaseRespository(ApiOption apiConfig, DatabaseContext databaseContext)
        {
            DbContext = databaseContext;
            Model = databaseContext.Set<T>();
            ApiConfig = apiConfig;
        }

        /// <summary>
        ///     Retrieve all data of repository
        /// </summary>
        /// <returns>IQueryable<T></returns>
        public IQueryable<T> FindAll()
        {
            return Model.AsNoTracking();
        }

        /// <summary>
        ///     Retrieve all data of repository by Condition
        /// </summary>
        /// <param name="expression">Condition</param>
        /// <returns>IQueryable<T></returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return Model.Where(expression).AsNoTracking();
        }

        /// <summary>
        ///     count record of repository by Condition
        /// </summary>
        /// <param name="expression">Condition</param>
        /// <returns>int</returns>
        public async Task<int> CountByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await Model.CountAsync(expression);
        }

        /// <summary>
        ///     Find data by id
        /// </summary>
        /// <param name="id">Table's primary key</param>
        /// <returns>Object</returns>
        public T FindOrFail(object id)
        {
            try
            {
                var model = Model.Find(id);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Find data by id
        /// </summary>
        /// <param name="id">Table's primary key</param>
        /// <returns>Object</returns>
        public T FindOrFailByCondition(Expression<Func<T, bool>> expression)
        {
            try
            {
                var model = Model.FirstOrDefault(expression);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Save a new entity in repository
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Object</returns>
        public T Create(T entity)
        {
            try
            {
                var model = Model.Add(entity);
                return model.Entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///     Update a entity in repository by entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>T</returns>
        public T UpdateByEntity(T entity)
        {
            try
            {
                Model.Update(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///     Update list entity in repository by entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>T</returns>
        public bool UpdateByEntityList(IEnumerable<T> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    Model.Update(entity);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///    Delete a entity in repository by entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Bool</returns>
        public bool DeleteByEntity(T entity)
        {
            try
            {
                Model.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public (List<T>, int) ReadAllLimit(int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, bool>> filter = null)
        {
            try
            {
                var all = DbContext.Set<T>();
                IQueryable<T> relevant = all.AsQueryable();
                int total = 0;
                if (filter != null)
                {
                    total = all.Where(filter).Count();
                    relevant = all.Where(filter).Skip(skip).Take(take);
                }
                else
                {
                    total = all.Count();
                    relevant = all.Skip(skip).Take(take);
                }
                if (orderBy != null)
                {
                    return (orderBy(relevant).ToList(), total);
                }
                (List<T>, int) result = (relevant.ToList(), total);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listobject"></param>
        /// <returns></returns>
        public async Task InsertBulk(List<T> listobject)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    if (listobject == null)
                        throw new ArgumentNullException(nameof(listobject));
                    await DbContext.BulkInsertAsync(listobject);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listobject"></param>
        /// <returns></returns>
        public async Task UpdateBulk(List<T> listobject)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    if (listobject == null)
                        throw new ArgumentNullException(nameof(listobject));
                    await DbContext.BulkUpdateAsync(listobject);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        ///     Save change to database
        /// </summary>
        public void SaveChange()
        {
            DbContext.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update Range
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool UpdateRange(List<T> range)
        {
            try
            {
                Model.UpdateRange(range);
                this.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
