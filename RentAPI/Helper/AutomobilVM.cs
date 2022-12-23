using System;

namespace RentAPI.Helper
{
    public class AutomobilVM
    {
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
    }
}
