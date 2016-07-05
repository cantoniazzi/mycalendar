using System;
using System.Linq;

namespace Agenda.DAL.Repository.Base
{
    interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Func<T, bool> predicate);
        T Find(params object[] key);
        void Update(T obj);
        void Save();
        void Insert(T obj);
        void Remove(Func<T, bool> predicate);
    }
}
