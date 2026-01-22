using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using System.Collections.Generic;
using System.Linq;

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        // "static" permet de conserver les données tant que l'API tourne
        private static List<Devise> lesDevises;

        // Constructeur : initialise la liste si elle est vide
        public DevisesController()
        {
            if (lesDevises == null)
            {
                lesDevises = new List<Devise>
                {
                    new Devise(1, "Dollar", 1.08),
                    new Devise(2, "Franc Suisse", 0.95),
                    new Devise(3, "Yen", 140.25)
                };
            }
        }

        // GET: api/Devises
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return lesDevises;
        }

        // GET: api/Devises/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById(int id)
        {
            Devise devise = lesDevises.FirstOrDefault(d => d.Id == id);

            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }

        // POST: api/Devises
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ajout simple (l'ID devrait idéalement être géré auto, mais on suit le TP)
            lesDevises.Add(devise);

            // Retourne 201 Created avec l'URL de la ressource créée
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        // PUT: api/Devises/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (id != devise.Id)
            {
                return BadRequest();
            }

            int index = lesDevises.FindIndex(d => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }

            lesDevises[index] = devise;
            return NoContent();
        }

        // DELETE: api/Devises/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Devise devise = lesDevises.FirstOrDefault(d => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }

            lesDevises.Remove(devise);
            return NoContent(); // Code 204
        }
    }
}