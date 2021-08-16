using System.Collections.Generic;

namespace Product.Api
{
    public interface IItemRepository<T> : IRepository<T> where T : class, IItem
    {
        T GetById(int id);
        List<T> GetListByIds(List<int> ids);
        void DeleteById(int id);
    }
}
