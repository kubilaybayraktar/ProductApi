using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public class BaseItemRepository<T> : BaseRepository<T>, IRepository<T> where T : class, IItem
    {
        public BaseItemRepository(DataContext context) : base(context)
        {
        }

        public T GetById(int id)
        {
            var list = dbSet.ToList();
            var tt = dbSet.Where(x => x.Id == id);
            return dbSet.SingleOrDefault(x => x.Id == id);
        }

        public List<T> GetListByIds(List<int> ids)
        {
            return dbSet.Where(x => ids.Contains(x.Id)).ToList();
        }

        public virtual void DeleteById(int id)
        {
            var entity = dbSet.SingleOrDefault(x => x.Id == id);
            dbSet.Remove(entity);
        }
    }
}
