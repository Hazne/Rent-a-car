using System;

namespace RentAPI.Models
{
    public class Komentar
    {
        public int KomentarId { get; set; }
        public string Opis { get; set; }
        public DateTime DatumKomentara { get; set; }
        public int KorisnikId { get; set; }
        public int AutomobilId { get; set; }

        public Korisnik Korisnik { get; set; }
        public Automobil Automobil { get; set; }
    }
}
