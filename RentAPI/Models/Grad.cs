using System.Collections.Generic;

namespace RentAPI.Models
{
    public class Grad
    {
        public int GradId { get; set; }
        public string ImeGrada { get; set; }
        public string PostanskiKod { get; set; }

        public ICollection<Izdavac> Izdavac { get; set; }
        public ICollection<Korisnik> Korisnik { get; set; }
    }
}
