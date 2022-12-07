using System.Collections;
using System.Collections.Generic;
using System;

namespace RentAPI.Models
{
    public class TipAutomobila
    {
        public int TipAutomobilaId { get; set; }
        public string ImeTipa { get; set; }

        public ICollection<Automobil> Automobil { get; set; }
    }
}
