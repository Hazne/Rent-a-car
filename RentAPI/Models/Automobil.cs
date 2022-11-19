using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

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
        public bool? Status { get; set; }
        public int IzdavacId { get; set; }
        public int TipGorivaId { get; set; }
        public int TipAutomobilaId { get; set; }
        public int ModelAutomobilaId { get; set; }

        public Izdavac Izdavac { get; set; }
        public TipGoriva TipGoriva { get; set; }
        public ModelAutomobila ModelAutomobila { get; set; }
        public TipAutomobila TipAutomobila { get; set; }
        public ICollection<Rezervisanje> Rezervisanje { get; set; }
        public ICollection<Komentar> Komentar { get; set; }
        public ICollection<Ocjena> Ocjena { get; set; }
    }
}
