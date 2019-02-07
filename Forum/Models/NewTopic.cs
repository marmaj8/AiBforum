using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class NewTopic
    {
        [DisplayName("Sekcja")]
        public int Section { get; set; }

        [DisplayName("Nazwa tematu")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "NAzwa musi się składać z 2-100 znaków")]
        public string Name { get; set; }

        [DisplayName("Tekst")]
        [Required(ErrorMessage = "Proszę podać tekst: ")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Tekst musi posiadać mniej niż 2000 znaków!")]
        public string Text { get; set; }

        [DisplayName("Autor")]
        public string Author { get; set; }
    }
}