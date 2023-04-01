using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class TwoFactorCodeModel
    {
        [Key]
        public int TwoFactorId { get; set; }
        public string Token { get; set; }
        public DateTime TrenutniDatum { get; set; }
        public DateTime DatumIsteka { get; set; }
        public bool IsProvjerena { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
