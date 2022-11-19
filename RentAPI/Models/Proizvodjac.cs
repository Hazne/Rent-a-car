using System.Collections.Generic;

namespace RentAPI.Models
{
    public class Proizvodjac
    {
        public int ProizvodjacId { get; set; }
        public string ImeProizvodjaca { get; set; }

        public ICollection<ModelAutomobila> ModelAutomobila { get; set; }
    }
}
