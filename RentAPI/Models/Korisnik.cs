using System.Collections.Generic;

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
        public int GradId { get; set; }
        public int? TipKorisnikaId { get; set; }

        public Grad Grad { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public ICollection<Rezervisanje> Rezervisanje { get; set; }
        public ICollection<Komentar> Komentar { get; set; }
        public ICollection<Ocjena> Ocjena { get; set; }
    }
}
