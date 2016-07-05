using System;
using System.Data.Entity;
using System.Linq;
using Agenda.DAL.Context;

namespace Agenda.DAL.Repository.Base
{
    public abstract class Repository<T> : IDisposable, IRepository<T> where T : class
    {
        DBContext db = new DBContext();

        public T Find(params object[] key)
        {
            return db.Set<T>().Find(key);
        }

        public IQueryable<T> Get(Func<T, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>();
        }

        public void Insert(T obj)
        {
            db.Set<T>().Add(obj);
        }

        public void Remove(Func<T, bool> predicate)
        {
            db.Set<T>()
                .Where(predicate).ToList()
                .ForEach(del => db.Set<T>().Remove(del));
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
