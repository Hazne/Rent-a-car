namespace RentAPI.Helper
{
    public class KorisnikVM
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public string brojMobitela { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string passwordSalt { get; set; }
        public int gradId { get; set; }
        public int? tipKorisnikaId { get; set; }

    }
}
