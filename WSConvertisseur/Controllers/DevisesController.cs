using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        // Liste statique pour simuler une base de données (persistance tant que l'API tourne)
        private static List<Devise> devises = new List<Devise>
        {
            new Devise(1, "Dollar", 1.08),
            new Devise(2, "Franc Suisse", 1.07),
            new Devise(3, "Yen", 120)
        };

        /// <summary>
        /// Récupère toutes les devises.
        /// </summary>
        /// <returns>La liste complète des devises</returns>
        /// <response code="200">Retourne la liste des devises</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<Devise> GetAll()
        {
            return devises;
        }

        /// <summary>
        /// Récupère une devise spécifique par son ID.
        /// </summary>
        /// <param name="id">L'identifiant de la devise recherchée</param>
        /// <returns>La devise correspondante ou une erreur 404</returns>
        /// <response code="200">La devise a été trouvée</response>
        /// <response code="404">Aucune devise trouvée avec cet ID</response>
        [HttpGet("{id}", Name = "GetDevise")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise = devises.FirstOrDefault(d => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return Ok(devise);
        }

        /// <summary>
        /// Ajoute une nouvelle devise.
        /// </summary>
        /// <param name="devise">L'objet devise à ajouter (le JSON dans le corps de la requête)</param>
        /// <returns>La devise créée avec son URI d'accès</returns>
        /// <response code="201">La devise a été créée avec succès</response>
        /// <response code="400">Le modèle de la devise est invalide (ex: champs manquants)</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            devises.Add(devise);
            // Retourne un code 201 Created avec l'URL pour récupérer la ressource (GetDevise)
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Modifie une devise existante.
        /// </summary>
        /// <param name="id">L'ID de la devise à modifier (doit correspondre à l'ID dans le corps)</param>
        /// <param name="devise">L'objet devise avec les nouvelles valeurs</param>
        /// <returns>Rien (204 No Content) si réussi</returns>
        /// <response code="204">Mise à jour réussie</response>
        /// <response code="400">L'ID de l'URL ne correspond pas à l'ID du corps ou modèle invalide</response>
        /// <response code="404">La devise à modifier n'existe pas</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (id != devise.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int index = devises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            devises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Supprime une devise.
        /// </summary>
        /// <param name="id">L'identifiant de la devise à supprimer</param>
        /// <returns>La devise supprimée ou 404</returns>
        /// <response code="200">Suppression réussie, retourne la devise supprimée</response>
        /// <response code="404">La devise à supprimer n'existe pas</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<Devise> Delete(int id)
        {
            Devise? devise = devises.FirstOrDefault(d => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            devises.Remove(devise);
            return Ok(devise);
        }
    }
}