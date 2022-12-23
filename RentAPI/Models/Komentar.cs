using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class Komentar
    {
        public int KomentarId { get; set; }
        public string Opis { get; set; }
        public DateTime DatumKomentara { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey(nameof(Automobil))]
        public int AutomobilId { get; set; }
        public Automobil Automobil { get; set; }

    }
}
