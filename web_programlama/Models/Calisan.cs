using web_programlama.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_programlama.Models
{
    public class Calisan
    {
        public int? Id { get; set; }
        public string? Ad { get; set; }
        public string? UzmanlikAlani { get; set; }
        public string? UygunlukSaatleri { get; set; }

        [ForeignKey("Salon")]
        public int? SalonId { get; set; }
        public Salon? Salon { get; set; }

        public ICollection<Islem>? Islemler { get; set; }
    }


}
