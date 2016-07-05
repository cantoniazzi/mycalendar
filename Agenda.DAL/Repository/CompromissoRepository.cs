using Agenda.DAL.Context;
using Agenda.DAL.Repository.Base;
using Agenda.Entities;
using DDay.iCal;
using DDay.iCal.Serialization;
using System;
using System.Data.Entity;
using System.Linq;

namespace Agenda.DAL.Repository
{
    public class CompromissoRepository : Repository<Compromisso>
    {
        private DBContext dbCompromisso = new DBContext();

        public IQueryable<Compromisso> GetAll()
        {
            return dbCompromisso.Set<Compromisso>();
        }

        public IQueryable<Compromisso> Get(Func<Compromisso, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public Compromisso Find(params object[] key)
        {
            return dbCompromisso.Set<Compromisso>().Find(key);
        }

        public void Update(Compromisso obj)
        {
            dbCompromisso.Entry(obj).State = EntityState.Modified;
        }

        public void Save()
        {
            dbCompromisso.SaveChanges();
        }

        public void Insert(Compromisso obj)
        {
            dbCompromisso.Set<Compromisso>().Add(obj);
        }

        public void Remove(Func<Compromisso, bool> predicate)
        {
            dbCompromisso.Set<Compromisso>()
                .Where(predicate).ToList()
                .ForEach(del => dbCompromisso.Set<Compromisso>().Remove(del));
        }

        public string Export(params object[] key)
        {
            string output = String.Empty;
            Compromisso compromisso = new Compromisso();

            compromisso = dbCompromisso.Set<Compromisso>().Find(key);

            var iCal = new iCalendar
            {
                Method = "PUBLISH",
                Version = "2.0"
            };
            var compromissoICal = iCal.Create<Event>();

            compromissoICal.Summary = compromisso.Titulo;
            compromissoICal.Start = new iCalDateTime(compromisso.HoraInicio);
            compromissoICal.Duration = TimeSpan.FromHours((compromisso.HoraFim - compromisso.HoraInicio).TotalMinutes);
            compromissoICal.Description = compromisso.Assunto;
            compromissoICal.Location = compromisso.Local;
            compromissoICal.UID = DateTime.Now.ToUniversalTime().ToString("yyyyMMddTHHmmssZ") + "@mysite.com";
            compromissoICal.Organizer = new Organizer("MAILTO:cassiosvaldo@gmail.com");

            // Create a serialization context and serializer factory.
            ISerializationContext ctx = new SerializationContext();
            ISerializerFactory factory = new DDay.iCal.Serialization.iCalendar.SerializerFactory();

            // Get a serializer for our object
            IStringSerializer serializer = factory.Build(iCal.GetType(), ctx) as IStringSerializer;

            output = serializer.SerializeToString(iCal);

            return output;

        }

        public void Dispose()
        {
            if (this.dbCompromisso != null)
            {
                dbCompromisso.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}   
