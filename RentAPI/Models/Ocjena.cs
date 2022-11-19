using System;

namespace RentAPI.Models
{
    public class Ocjena
    {
        public int OcjenaId { get; set; }
        public int BrojOcjene { get; set; }
        public int KorisnikId { get; set; }
        public int AutomobilId { get; set; }
        public DateTime DatumOcjene { get; set; }

        public Korisnik Korisnik { get; set; }
        public Automobil Automobil { get; set; }
    }
}
