using web_programlama.Models;
using System.Collections.Generic;

namespace web_programlama.Models
{
    public class Salon
    {
        public int? Id { get; set; }
        public string? Ad { get; set; }
        public string? CalismaSaatleri { get; set; }

        public ICollection<Islem>? Islemler { get; set; } = new List<Islem>();
        public ICollection<Calisan>? Calisanlar { get; set; } = new List<Calisan>();
    }

}
