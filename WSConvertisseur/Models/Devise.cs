using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {
        public int Id { get; set; }

        [Required]
        public string NomDevise { get; set; }

        public double Taux { get; set; }

        public Devise()
        {
            // Constructeur vide requis pour la sérialisation
        }

        public Devise(int id, string nomDevise, double taux)
        {
            Id = id;
            NomDevise = nomDevise;
            Taux = taux;
        }
    }
}