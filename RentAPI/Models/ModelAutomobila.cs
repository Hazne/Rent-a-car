using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class ModelAutomobila
    {
        public int ModelAutomobilaId { get; set; }
        public string ImeModela { get; set; }
        public string Opis { get; set; }
        [ForeignKey(nameof(Proizvodjac))]
        public int? ProizvodjacId { get; set; }
        public Proizvodjac Proizvodjac { get; set; }

    }
}
