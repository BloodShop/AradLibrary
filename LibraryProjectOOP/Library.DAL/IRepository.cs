using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.DAL
{
    public interface IRepository<T> where T : class
    {
        T Add(T item, bool addAction = true);
        IQueryable<T> GetAvailable();
        T Get(Guid id);
        T Update(T old, T @new);
        T Delete(T item, Guid id = default(Guid));
    }
}