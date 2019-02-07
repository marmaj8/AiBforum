using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    [MetadataType(typeof(SectionExtension))]
    partial class Section
    {
    }
    public class SectionExtension
    {
        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nazwa musi się mieścić w przedziale od 1 do 200 znaków")]
        [RegularExpression("[a-zA-Z0-9 ]{2,30}", ErrorMessage = "Tylko litery lub liczby!")]
        public string name { get; set; }

        [DisplayName("Grupy - odczyt")]
        [Required(ErrorMessage = "Proszę wybrać grupę")]
        public int readGroupFk { get; set; }

        [DisplayName("Grupy - zapis")]
        [Required(ErrorMessage = "Proszę wybrać grupę")]
        public int writeGroupFk { get; set; }

        [DisplayName("ID")]
        public int idSection { get; set; }
    }
}