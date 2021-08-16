using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public interface IRepository<T> where T : class, IEntity
    {
        T GetByField(string field, object value);

        IQueryable<T> GetListByField(string field, object value);

        IQueryable<T> GetList();

        IQueryable<T> GetListContains(string field, string value);

        T Insert(T entity);

        void InsertList(List<T> entities);

        T Update(T entity);

        void UpdateList(List<T> entities);

        void Delete(T entity);

        void DeleteList(List<T> entities);
    }
}
