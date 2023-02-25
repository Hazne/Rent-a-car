using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class Automobil
    {
        public int AutomobilId { get; set; }
        public string Tablice { get; set; }
        public byte[] Slika { get; set; }
        public int BrojAutomobila { get; set; }
        public double CijenaPoDanu { get; set; }
        public string Opis { get; set; }
        public DateTime DatumProizvodnje { get; set; }
        public string Kolometraza { get; set; }
        public string Vuca { get; set; }
        public int BrojSjedala { get; set; }

        [ForeignKey(nameof(Izdavac))]
        public int IzdavacId { get; set; }
        public Izdavac Izdavac { get; set; }

        [ForeignKey(nameof(TipGoriva))]
        public int TipGorivaId { get; set; }
        public TipGoriva TipGoriva { get; set; }

        [ForeignKey(nameof(TipAutomobila))]
        public int TipAutomobilaId { get; set; }
        public TipAutomobila TipAutomobila { get; set; }

        [ForeignKey(nameof(ModelAutomobila))]
        public int ModelAutomobilaId { get; set; }
        public ModelAutomobila ModelAutomobila { get; set; }

    }
}
