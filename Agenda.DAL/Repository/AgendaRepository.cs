using Agenda.DAL.Context;
using Agenda.DAL.Repository.Base;
using System;
using System.Data.Entity;
using System.Linq;

namespace Agenda.DAL.Repository
{
    public class AgendaRepository : Repository<Entities.Agenda>
    {
        private DBContext dbAgenda = new DBContext();
        
        public IQueryable<Agenda.Entities.Agenda> GetAll()
        {
            return dbAgenda.Set<Agenda.Entities.Agenda>();
        }

        public IQueryable<Agenda.Entities.Agenda> Get(Func<Agenda.Entities.Agenda, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public Entities.Agenda Find(params object[] key)
        {
            return dbAgenda.Set<Agenda.Entities.Agenda>().Find(key);
        }

        public void Update(Entities.Agenda obj)
        {
            dbAgenda.Entry(obj).State = EntityState.Modified;
        }

        public void Save()
        {
            dbAgenda.SaveChanges();
        }

        public void Insert(Entities.Agenda obj)
        {
            dbAgenda.Set<Agenda.Entities.Agenda>().Add(obj);
        }

        public void Remove(Func<Agenda.Entities.Agenda, bool> predicate)
        {
            dbAgenda.Set<Entities.Agenda>()
                .Where(predicate).ToList()
                .ForEach(del => dbAgenda.Set<Agenda.Entities.Agenda>().Remove(del));
        }

        public void Dispose()
        {
            if (this.dbAgenda != null)
            {
                dbAgenda.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
