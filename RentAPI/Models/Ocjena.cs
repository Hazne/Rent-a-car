using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class Ocjena
    {
        public int OcjenaId { get; set; }
        public int BrojOcjene { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey(nameof(Automobil))]
        public int AutomobilId { get; set; }
        public Automobil Automobil { get; set; }
        public DateTime DatumOcjene { get; set; }
    }
}
