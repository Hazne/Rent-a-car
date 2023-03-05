using RentAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace RentAPI.Helper
{
    public class OcjenaVM
    {
        public int BrojOcjene { get; set; }

        public int KorisnikId { get; set; }
        
        public int AutomobilId { get; set; }
        public DateTime DatumOcjene { get; set; }
        public int RezervisanjeId { get; set; }
    }
}
