using MVC_Eventos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC_Eventos.Controllers
{
    public class EventosController : Controller
    {
        // GET: Eventos
        public ActionResult Index()
        {
            IEnumerable<EventosModel> listaEventos;
            HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Eventos").Result;
            listaEventos = response.Content.ReadAsAsync<IEnumerable<EventosModel>>().Result;
            return View(listaEventos);
        }


        public ActionResult ListarEventos()
        {
            IEnumerable<EventosModel> listaEventos;
            HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Eventos").Result;
            listaEventos = response.Content.ReadAsAsync<IEnumerable<EventosModel>>().Result;
            return View(listaEventos);
        }

        [HttpGet]
        public ActionResult Create() {
            EventosModel emp = EmptyEvento();
            return PartialView("Create",emp);
        }

        public EventosModel EmptyEvento() {
            EventosModel ev = new EventosModel { ParticipanteEvento = new List<ParticipanteEventoModel>() };
            for (int i = 0; i < 5; i++)
            {
                ParticipanteEventoModel pt = new ParticipanteEventoModel { EventoID = 0, Participante = "" };
                ev.ParticipanteEvento.Add(pt);
            }
            return ev;
        }

        [HttpPost]
        [ActionName ("Create")]
        public ActionResult Create( EventosModel evento)
        {
            
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalSettings.WebApiClient.PostAsJsonAsync("Eventos", evento).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    EventosModel eventResult = JsonConvert.DeserializeObject<EventosModel>(result);
                    foreach (var item in evento.ParticipanteEvento)
                    {
                        if (!String.IsNullOrEmpty(item.Participante) && !item.Participante.Equals(""))
                        {
                            ParticipanteEventoModel pem = new ParticipanteEventoModel();
                            pem.EventoID = eventResult.EventoID;
                            pem.Participante = item.Participante;
                            HttpResponseMessage responseParticipante = GlobalSettings.WebApiClient.PostAsJsonAsync("ParticipanteEventos", pem).Result;
                        }
                    }
                    
                }
                TempData["SuccessMessage"] = "Evento Guardado";
                return RedirectToAction("Index");
            }
            return PartialView("Create", evento);
        }

        public ActionResult Details(int id)
        {
            if (id == 0)
                return View(new EventosModel());
            else
            {
                HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Eventos/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<EventosModel>().Result);
            }
        }
    }
}