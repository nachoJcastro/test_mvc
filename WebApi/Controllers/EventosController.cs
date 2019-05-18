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
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EventosController : ApiController
    {
        private EventosDB db = new EventosDB();

        // GET: api/Eventos
        public IQueryable<Eventos> GetEventos()
        {
            return db.Eventos;
        }

        // GET: api/Eventos/5
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult GetEventos(int id)
        {
            Eventos eventos = db.Eventos.Find(id);
            if (eventos == null)
            {
                return NotFound();
            }

            return Ok(eventos);
        }

        // PUT: api/Eventos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventos(int id, Eventos eventos)
        {
            if (id != eventos.EventoID)
            {
                return BadRequest();
            }

            db.Entry(eventos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventosExists(id))
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

        // POST: api/Eventos
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult PostEventos(Eventos eventos)
        {
            db.Eventos.Add(eventos);
          
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EventosExists(eventos.EventoID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eventos.EventoID }, eventos);
        }

        // DELETE: api/Eventos/5
        [ResponseType(typeof(Eventos))]
        public IHttpActionResult DeleteEventos(int id)
        {
            Eventos eventos = db.Eventos.Find(id);
            if (eventos == null)
            {
                return NotFound();
            }

            db.Eventos.Remove(eventos);
            db.SaveChanges();

            return Ok(eventos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventosExists(int id)
        {
            return db.Eventos.Count(e => e.EventoID == id) > 0;
        }
    }
}