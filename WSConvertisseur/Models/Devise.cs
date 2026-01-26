using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {
        public int Id { get; set; }

        [Required]
        public string? NomDevise { get; set; } // Modification ici : string? (Nullable)

        public double Taux { get; set; }

        public Devise()
        {
            // Constructeur vide requis pour la sérialisation
        }

        public Devise(int id, string? nomDevise, double taux) // Modification ici : string?
        {
            Id = id;
            NomDevise = nomDevise;
            Taux = taux;
        }

        // --- AJOUT OBLIGATOIRE POUR LES TESTS ---
        public override bool Equals(object? obj)
        {
            return obj is Devise devise &&
                   Id == devise.Id &&
                   NomDevise == devise.NomDevise &&
                   Taux == devise.Taux;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, NomDevise, Taux);
        }
    }
}