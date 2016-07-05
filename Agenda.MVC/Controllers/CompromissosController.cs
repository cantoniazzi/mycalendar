using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Agenda.Entities;
using Agenda.DAL.Repository;
using System.IO;
using System.Text;
using System.Globalization;

namespace Agenda.MVC.Controllers
{
    public class CompromissosController : FilterController
    {
        //private CompromissoRepository dbCompromisso = new CompromissoRepository();
        //private AgendaRepository dbAgenda = new AgendaRepository();

        // GET: Compromisso
        [Authorize]
        public ActionResult Index(int? SelectAgenda, int? SelectMonth, string SearchData)
        {
            var agendas = dbAgenda.GetAll().ToList();
            var compromissos = base.ApplyFilter(SelectAgenda, SearchData, agendas, dbCompromisso.GetAll().ToList());

            return View(compromissos);
        }

        // GET: Compromisso/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var compromissos = dbCompromisso.Find(id);

            if (compromissos == null)
            {
                return HttpNotFound();
            }
            return View(compromissos);
        }

        // GET: Compromisso/Create
        [Authorize]
        public ActionResult Create(int? agendaID)
        {
            if (agendaID == null) agendaID = 0;
            return View(new Compromisso {
                AgendaID = (int)agendaID,
                HoraInicio = DateTime.Now,
                HoraFim = DateTime.Now
            });
        }

        // POST: Compromisso/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgendaID,ID,Titulo,Assunto,Local,HoraInicio,HoraFim")] Entities.Compromisso compromisso)
        {
            if (ModelState.IsValid)
            {
                //db.Compromissoes.Add(compromisso);
                dbCompromisso.Insert(compromisso);

                //db.SaveChanges();
                dbCompromisso.Save();
                return RedirectToAction("Details", "Agendas", new { id = compromisso.AgendaID });
            }

            return View(compromisso);
        }

        // GET: Compromisso/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Compromisso compromisso = dbCompromisso.Find(id);

            if (compromisso == null)
            {
                return HttpNotFound();
            }

            return View(compromisso);
        }

        // POST: Compromisso/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgendaID, ID,Titulo,Assunto,Local,HoraInicio,HoraFim")] Compromisso compromisso)
        {
            if (ModelState.IsValid)
            {
                dbCompromisso.Update(compromisso);
                dbCompromisso.Save();

                //return RedirectToAction("Details", "Agendas", new { id = compromisso.AgendaID });
            }
            return View(compromisso);
        }

        // GET: Compromisso/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Compromisso compromisso = dbCompromisso.Find(id);

            if (compromisso == null)
            {
                return HttpNotFound();
            }
            return View(compromisso);
        }

        // POST: Compromisso/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compromisso compromisso = dbCompromisso.Find(id);

            int agendaID = compromisso.AgendaID;

            dbCompromisso.Remove(c => c == compromisso);
            dbCompromisso.Save();
            return RedirectToAction("Index", "Agendas", new { id = agendaID });
        }

        [Authorize]
        public FileStreamResult ExportCompromisso(int? id)
        {
            string compromisso_iCal = dbCompromisso.Export(id);
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(compromisso_iCal));
            return File(stream, "text/plain", id.ToString() + ".ics");            
        }

        protected override void Dispose(bool disposing)
        {
            dbAgenda.Dispose();
            dbCompromisso.Dispose();
            base.Dispose(disposing);
        }
    }
}
