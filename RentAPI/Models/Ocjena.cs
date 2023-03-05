using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RentAPI.Models
{
    public class Ocjena
    {
        public int OcjenaId { get; set; }
        public int BrojOcjene { get; set; }
        [JsonIgnore]

        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey(nameof(Automobil))]
        public int AutomobilId { get; set; }
        public Automobil Automobil { get; set; }
        public DateTime DatumOcjene { get; set; }

        [ForeignKey(nameof(Rezervisanje))]
        public int RezervisanjeId { get; set; }
        public Rezervisanje Rezervisanje { get; set; }
    }
}
