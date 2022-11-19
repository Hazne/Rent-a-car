using System;

namespace RentAPI.Models
{
    public class Rezervisanje
    {
        public int RezervisanjeId { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public int KorisnikId { get; set; }
        public int AutomobilId { get; set; }
        public bool? StatusOcjene { get; set; }
        public bool? StatusKomentara { get; set; }

        public virtual Korisnik Korisnik { get; set; }
        public virtual Automobil Automobil { get; set; }
    }
}
