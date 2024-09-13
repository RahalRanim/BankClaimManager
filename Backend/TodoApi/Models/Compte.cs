namespace TodoApi.Models
{
    public class Compte
    {
        public int IdCompte { get; set; }
        public string MoyenPayement { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public int IdClient { get; set; }
    }
}
