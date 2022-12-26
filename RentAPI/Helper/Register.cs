using RentAPI.Models;

namespace RentAPI.Helper
{
    public class Register
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string BrojMobitela { get; set; }
        public string Grad { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public TipKorisnika TipKorisnika { get; set; } 

    }
}
