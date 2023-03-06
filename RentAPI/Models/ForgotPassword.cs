using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class ForgotPassword
    {
        [Key]
        public int ForgotPasswordId { get; set; }
        public string Token { get; set; }
        public DateTime DatumIstekaTokena { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
