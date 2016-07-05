using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Agenda.DAL.Repository;
using HtmlAgilityPack;
using Agenda.MVC.util;

namespace Agenda.MVC.Controllers
{
    public class AgendasController : FilterController
    {
        private AgendaRepository dbAgenda = new AgendaRepository();

        // GET: Agenda
        [Authorize]
        public ActionResult Index()
        {
            var agendas = dbAgenda.GetAll().Where(c => c.UserName == User.Identity.Name).ToList();
            
            return View(agendas);
        }

        // GET: Agenda/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Agenda agenda = dbAgenda.Find(id);

            if (agenda == null)
            {
                return HttpNotFound();
            }

            ViewBag.Compromissos =  base.ApplyFilter(null, null, dbAgenda.GetAll(), agenda.Compromissos);

            return View(agenda);
        }

        // GET: Agendas/Create
        [Authorize]
        public ActionResult Create()
        {
            return View(new Entities.Agenda() { UserName = User.Identity.Name });
        }

        // POST: Agenda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgendaID,Titulo,UserName,Cor,Icone")] Entities.Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                dbAgenda.Insert(agenda);
                dbAgenda.Save();
                return RedirectToAction("Index");
            }

            return View(agenda);
        }

        // GET: Agendas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id, string actionRedir)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Entities.Agenda agenda = dbAgenda.Find(id);

            //param to redirect
            ViewBag.ActionRedir = actionRedir;

            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agendas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgendaID,Titulo,UserName,Cor,Icone")] Entities.Agenda agenda, string actionRedir)
        {
            if (ModelState.IsValid)
            {
                dbAgenda.Update(agenda);
                dbAgenda.Save();
                
                if(actionRedir == "Details")
                    return RedirectToAction(actionRedir, new { id = agenda.AgendaID});

                return RedirectToAction("index");
            }
            return View(agenda);
        }

        // GET: Agendas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entities.Agenda agenda = dbAgenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agendas/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entities.Agenda agenda = dbAgenda.Find(id);

            dbAgenda.Remove(a => a == agenda);

            dbAgenda.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            dbAgenda.Dispose();
            base.Dispose(disposing);
        }

        // GET: Agendas/ShowIcons
        [Authorize]
        public ActionResult ShowIcons()
        {
            var url = "http://fontawesome.io/icons/";
            var data = new MyWebClient().DownloadString(url);
            var document = new HtmlDocument();
            document.LoadHtml(data);
            
            var icons = document.DocumentNode.SelectNodes("//i").Select(
                i => i.OuterHtml.Replace("\" aria-hidden=\"true\"", " fa-2x\"")
            );
            
            return View(icons);
        }

        [Authorize]
        public ActionResult GetCompromissos(int? id)
        {
            var compromissos = dbAgenda.Find(id).Compromissos;
            return View(compromissos);
        }

    }
}
