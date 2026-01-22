namespace WSConvertisseur.Models
{
    public class Devise
    {
        private int id;
        private string nomdevise;
        private double taux;

        public Devise()
        {
        }

        public Devise(int id, string nomdevise, double taux)
        {
            this.Id = id;
            this.Nomdevise = nomdevise;
            this.Taux = taux;
        }

        public int Id { get => id; set => id = value; }
        public string Nomdevise { get => nomdevise; set => nomdevise = value; }
        public double Taux { get => taux; set => taux = value; }
    }
}
