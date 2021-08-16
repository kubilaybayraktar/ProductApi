using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Product.Api
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        internal DataContext DbContext;
        internal DbSet<T> dbSet;
        public DbContextOptionsBuilder<DataContext> options;
        public string ConnectionString { get; set; }

        public BaseRepository(DataContext context)
        {
            options = new();
            DbContext = context;
            dbSet = DbContext.Set<T>();
        }

        public SqlParameter CreateParameter(string parameterName, object value, SqlDbType? sqlDbType = null, string typeName = "", ParameterDirection? direction = null, DbType? dbType = null)
        {
            SqlParameter parameter = new SqlParameter(parameterName, value);

            if (sqlDbType.HasValue)
            {
                parameter.SqlDbType = sqlDbType.Value;
            }

            if (dbType.HasValue)
            {
                parameter.DbType = dbType.Value;
            }

            if (!string.IsNullOrEmpty(typeName))
            {
                parameter.TypeName = typeName;
            }

            if (direction.HasValue)
            {
                parameter.Direction = direction.Value;
            }

            return parameter;
        }

        #region Get

        public T GetByField(string field, object value)
        {
            var parameter = Expression.Parameter(typeof(T), field);
            var member = Expression.Property(parameter, field);
            var constant = Expression.Constant(value);
            var body = Expression.Equal(member, constant);
            var finalExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

            List<T> result = dbSet.Where(finalExpression).ToList();

            return result.FirstOrDefault();
        }

        public IQueryable<T> GetListByField(string field, object value)
        {
            var parameter = Expression.Parameter(typeof(T), field);
            var member = Expression.Property(parameter, field);
            var constant = Expression.Constant(value);
            var body = Expression.Equal(member, constant);
            var finalExpression = Expression.Lambda<Func<T, bool>>(body, parameter);

            return dbSet.Where(finalExpression);
        }
        #endregion

        #region Get Many
        /// <summary>
        /// Get entity list
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns></returns>
        public IQueryable<T> GetList()
        {
            return dbSet;
        }


        /// <summary>
        /// Get list of entities by a custom field query
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="field">Field to query</param>
        /// <param name="value">Filter value</param>
        /// <returns></returns>
        public IQueryable<T> GetListContains(string field, string value)
        {
            var parameter = Expression.Parameter(typeof(T), field);
            var getter = Expression.Property(parameter, field);

            //ToString is not supported in Linq-To-Entities, throw an exception if the property is not a string.
            if (getter.Type != typeof(string))
                throw new ArgumentException("Property must be a string");

            //string.Contains with string parameter.
            var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsCall = Expression.Call(getter, stringContainsMethod,
                Expression.Constant(value, typeof(string)));

            var finalExpression = Expression.Lambda<Func<T, bool>>(containsCall, parameter);

            return dbSet.Where(finalExpression);
        }

        #endregion

        #region Insert
        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public virtual T Insert(T entity)
        {
            dbSet.Add(entity);

            return entity;
        }

        /// <summary>
        /// Insert entities range
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entities">Entities</param>
        public virtual void InsertList(List<T> entities)
        {
            dbSet.AddRange(entities);
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an entity
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public virtual T Update(T entity)
        {
            SafeAttach(entity);
            return entity;
        }

        /// <summary>
        /// Update entities range
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entities">Entities</param>
        public virtual void UpdateList(List<T> entities)
        {
            entities.ForEach(x =>
            {
                SafeAttach(x);
            });
        }

        private void SafeAttach(T x)
        {
            bool attach = true;
            if (x is ProductAttribute)
            {
                ProductAttribute productAttribute = x as ProductAttribute;
                var attachedEntity = DbContext.ChangeTracker.Entries<ProductAttribute>().FirstOrDefault(e => e.Entity.ProductId == productAttribute.ProductId && e.Entity.AttributeId == productAttribute.AttributeId);
                attach = attachedEntity == null;
            }

            if (attach)
            {
                dbSet.Attach(x);
                DbContext.Entry(x).State = EntityState.Modified;
            }
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }



        /// <summary>
        /// Delete entities range
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entities">Entities</param>
        /// <returns></returns>
        public virtual void DeleteList(List<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        #endregion
    }
}
