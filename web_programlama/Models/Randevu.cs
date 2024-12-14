﻿using web_programlama.Models;
using System;

namespace web_programlama.Models
{
    public class Randevu
    {
        public int? Id { get; set; }
        public DateTime RandevuZamani { get; set; }

        public int? IslemId { get; set; }
        public Islem? Islem { get; set; } = new Islem();

        public int? CalisanId { get; set; }
        public Calisan? Calisan { get; set; } = new Calisan();

        public string? KullaniciId { get; set; }
    }

}
