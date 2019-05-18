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
    public class ParticipanteEventosController : ApiController
    {
        private EventosDB db = new EventosDB();

        // GET: api/ParticipanteEventos
        public IQueryable<ParticipanteEvento> GetParticipanteEvento()
        {
            return db.ParticipanteEvento;
        }

        // GET: api/ParticipanteEventos/5
        [ResponseType(typeof(ParticipanteEvento))]
        public IHttpActionResult GetParticipanteEvento(int id)
        {
            ParticipanteEvento participanteEvento = db.ParticipanteEvento.Find(id);
            if (participanteEvento == null)
            {
                return NotFound();
            }

            return Ok(participanteEvento);
        }

        // PUT: api/ParticipanteEventos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipanteEvento(int id, ParticipanteEvento participanteEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participanteEvento.EventoID)
            {
                return BadRequest();
            }

            db.Entry(participanteEvento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteEventoExists(id))
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

        // POST: api/ParticipanteEventos
        [ResponseType(typeof(ParticipanteEvento))]
        public IHttpActionResult PostParticipanteEvento(ParticipanteEvento participanteEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParticipanteEvento.Add(participanteEvento);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ParticipanteEventoExists(participanteEvento.EventoID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = participanteEvento.EventoID }, participanteEvento);
        }

        // DELETE: api/ParticipanteEventos/5
        [ResponseType(typeof(ParticipanteEvento))]
        public IHttpActionResult DeleteParticipanteEvento(int id)
        {
            ParticipanteEvento participanteEvento = db.ParticipanteEvento.Find(id);
            if (participanteEvento == null)
            {
                return NotFound();
            }

            db.ParticipanteEvento.Remove(participanteEvento);
            db.SaveChanges();

            return Ok(participanteEvento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipanteEventoExists(int id)
        {
            return db.ParticipanteEvento.Count(e => e.EventoID == id) > 0;
        }
    }
}