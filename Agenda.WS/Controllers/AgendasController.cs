using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Agenda.DAL.Context;

namespace Agenda.WS.Controllers
{
    public class AgendasController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/Agendas
        public IQueryable<Entities.Agenda> GetAgendas()
        {
            return db.Agendas;
        }

        // GET: api/Agendas/5
        [ResponseType(typeof(Entities.Agenda))]
        public IHttpActionResult GetAgenda(int id)
        {
            Entities.Agenda agenda = db.Agendas.Find(id);
            if (agenda == null)
            {
                return NotFound();
            }

            return Ok(agenda);
        }

        // PUT: api/Agendas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgenda(int id, Entities.Agenda agenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agenda.AgendaID)
            {
                return BadRequest();
            }

            db.Entry(agenda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Agendas
        [ResponseType(typeof(Entities.Agenda))]
        public IHttpActionResult PostAgenda(Entities.Agenda agenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Agendas.Add(agenda);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agenda.AgendaID }, agenda);
        }

        // DELETE: api/Agendas/5
        [ResponseType(typeof(Entities.Agenda))]
        public IHttpActionResult DeleteAgenda(int id)
        {
            Entities.Agenda agenda = db.Agendas.Find(id);
            if (agenda == null)
            {
                return NotFound();
            }

            db.Agendas.Remove(agenda);
            db.SaveChanges();

            return Ok(agenda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgendaExists(int id)
        {
            return db.Agendas.Count(e => e.AgendaID == id) > 0;
        }
    }
}