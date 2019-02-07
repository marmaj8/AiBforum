using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    [MetadataType(typeof(PostExtension))]
    partial class Post
    {
    }
    public class PostExtension
    {
        [DisplayName("Tekst")]
        [Required(ErrorMessage = "Proszę podać tekst: ")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Tekst musi posiadać mniej niż 2000 znaków!")]
        public string text { get; set; }

        [DisplayName("ID")]
        public int idPost { get; set; }

        [DisplayName("Autor")]
        public int author { get; set; }

        [DisplayName("Temat")]
        public int topic { get; set; }

        [DisplayName("Data")]
        public Nullable<System.DateTime> date { get; set; }
    }
}
