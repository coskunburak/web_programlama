namespace web_programlama.Models
{
    public class Islem
    {
        public int? Id { get; set; }
        public string? Ad { get; set; }
        public int? Sure { get; set; }
        public decimal? Ucret { get; set; }

        public int? SalonId { get; set; }
        public Salon? Salon { get; set; } = new Salon();

        public ICollection<Calisan>? Calisanlar { get; set; } = new List<Calisan>();
    }

}
