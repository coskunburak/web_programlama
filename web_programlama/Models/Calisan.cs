using web_programlama.Models;
using System.Collections.Generic;

namespace web_programlama.Models
{
    public class Calisan
    {
        public int? Id { get; set; }
        public string? Ad { get; set; }
        public string? UzmanlikAlani { get; set; }
        public string? UygunlukSaatleri { get; set; }

        public int? SalonId { get; set; }
        public Salon? Salon { get; set; } = new Salon();

        public ICollection<Islem>? Islemler { get; set; } = new List<Islem>();
    }


}
