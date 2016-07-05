using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Agenda.DAL.Context;
using Agenda.Entities;

namespace Agenda.WS.Controllers
{
    public class CompromissosController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/Compromissos
        public IQueryable<Compromisso> GetCompromissos()
        {
            return db.Compromissos;
        }

        // GET: api/Compromissos/5
        [ResponseType(typeof(Compromisso))]
        public IHttpActionResult GetCompromisso(int id)
        {
            Compromisso compromisso = db.Compromissos.Find(id);
            if (compromisso == null)
            {
                return NotFound();
            }

            return Ok(compromisso);
        }

        // PUT: api/Compromissos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompromisso(int id, Compromisso compromisso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compromisso.ID)
            {
                return BadRequest();
            }

            db.Entry(compromisso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompromissoExists(id))
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

        // POST: api/Compromissos
        [ResponseType(typeof(Compromisso))]
        public IHttpActionResult PostCompromisso(Compromisso compromisso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Compromissos.Add(compromisso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = compromisso.ID }, compromisso);
        }

        // DELETE: api/Compromissos/5
        [ResponseType(typeof(Compromisso))]
        public IHttpActionResult DeleteCompromisso(int id)
        {
            Compromisso compromisso = db.Compromissos.Find(id);
            if (compromisso == null)
            {
                return NotFound();
            }

            db.Compromissos.Remove(compromisso);
            db.SaveChanges();

            return Ok(compromisso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompromissoExists(int id)
        {
            return db.Compromissos.Count(e => e.ID == id) > 0;
        }
    }
}