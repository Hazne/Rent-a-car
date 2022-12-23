using RentAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPI.Helper
{
    public class IzdavacVM
    {
        public string ImeIzdavaca { get; set; }
        public string BrojMobitela { get; set; }
        public string Adresa { get; set; }
        public string VrijemeOtvaranja { get; set; }
        public string VrijemeZatvaranja { get; set; }
        public string Opis { get; set; }
        public int GradId { get; set; }
    }
}
