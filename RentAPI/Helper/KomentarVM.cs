using RentAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace RentAPI.Helper
{
    public class KomentarVM
    {
        public string Opis { get; set; }
        public DateTime DatumKomentara { get; set; }
        public int KorisnikId { get; set; }
        public int AutomobilId { get; set; }
        public int RezervisanjeId { get; set; }
    }
}
