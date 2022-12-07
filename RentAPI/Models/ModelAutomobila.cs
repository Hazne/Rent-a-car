using System.Collections.Generic;
using System;

namespace RentAPI.Models
{
    public class ModelAutomobila
    {
        public int ModelAutomobilaId { get; set; }
        public string ImeModela { get; set; }
        public string Opis { get; set; }
        public int ProizvodjacId { get; set; }

        public Proizvodjac Proizvodjac { get; set; }
        public ICollection<Automobil> Automobil { get; set; }
    }
}
