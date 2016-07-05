using Agenda.DAL.Repository;
using Agenda.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda.MVC.Controllers
{
    public abstract class FilterController : Controller
    {
        protected CompromissoRepository dbCompromisso = new CompromissoRepository();
        protected AgendaRepository dbAgenda = new AgendaRepository();

        public PartialViewResult Filter(int? SelectAgenda, string SearchData)
        {
            var compromissos = dbCompromisso.GetAll().ToList();
            var agendas = dbAgenda.GetAll().ToList();

            var comp = ApplyFilter(SelectAgenda, SearchData, agendas, compromissos);


            return PartialView("_CompromissoList", comp);
        }

        protected IEnumerable<Compromisso> ApplyFilter(int? SelectAgenda, string SearchData, IEnumerable<Entities.Agenda> agendas, IEnumerable<Compromisso> compromissos) {
            DateTime dataSelected;

            if (SelectAgenda != null)
                compromissos = compromissos.Where(c => c.AgendaID == SelectAgenda).ToList();

            if (DateTime.TryParse(SearchData, out dataSelected))
                compromissos = compromissos.Where(
                    c => c.HoraInicio.Day == dataSelected.Day &&
                    c.HoraInicio.Month == dataSelected.Month &&
                    c.HoraInicio.Year == dataSelected.Year
                    ).ToList();

            compromissos = compromissos.OrderBy(c => c.Titulo).ToList();

            //create filters
            ViewBag.SearchData = SearchData;
            ViewBag.AgendaID = SelectAgenda;
            ViewBag.SelectAgenda = new SelectList(agendas, "AgendaID", "Titulo", ViewBag.AgendaID);

            return compromissos;
        }

    }
}