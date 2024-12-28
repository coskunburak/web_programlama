using web_programlama.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_programlama.Models
{
    public class Randevu
    {
        public int? Id { get; set; }
        public DateTime RandevuZamani { get; set; }

        [ForeignKey(nameof(Islem))]
        public int? IslemId { get; set; }
        public Islem? Islem { get; set; }

        [ForeignKey(nameof(Calisan))]
        public int? CalisanId { get; set; }
        public Calisan? Calisan { get; set; }

        public string? KullaniciId { get; set; }
    }

}
