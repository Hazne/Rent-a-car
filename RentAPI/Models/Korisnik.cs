using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class Korisnik
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojMobitela { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }

        [ForeignKey (nameof(Grad))]
        public int GradId { get; set; }
        public Grad Grad { get; set; }


        [ForeignKey(nameof(TipKorisnika))]
        public int? TipKorisnikaId { get; set; }
        public TipKorisnika TipKorisnika { get; set; }


    }
}
