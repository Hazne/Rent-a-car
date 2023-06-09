﻿using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Models
{
    public class Izdavac
    {
        public int IzdavacId { get; set; }
        public string ImeIzdavaca { get; set; }
        public string BrojMobitela { get; set; }
        public string Adresa { get; set; }
        public string VrijemeOtvaranja { get; set; }
        public string VrijemeZatvaranja { get; set; }
        public string Opis { get; set; }

        [ForeignKey(nameof(Grad))]
        public int GradId { get; set; }
        public Grad Grad { get; set; }
        
    }
}
