using System;

namespace RentAPI.Helper
{
    public class RezervisanjeVM
    {
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public bool? StatusOcjene{ get; set; }
        public bool? StatusKomentara { get; set; }
        public int KorisnikId { get; set; }
        public int AutomobilId { get; set; }

    }
}
