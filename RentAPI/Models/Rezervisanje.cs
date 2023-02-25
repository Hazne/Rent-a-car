using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class Rezervisanje
    {
        public int RezervisanjeId { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public bool? StatusOcjene { get; set; }
        public bool? StatusKomentara { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public  Korisnik Korisnik { get; set; }
        [ForeignKey(nameof(Automobil))]
        public int AutomobilId { get; set; }
        public  Automobil Automobil { get; set; }
    }
}
