using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Forum.Models
{
    [MetadataType(typeof(GroupExtension))]
    partial class Group
    {
    }
    public class GroupExtension
    {
        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Nazwa musi posiadać od 1 do 30 znaków")]
        [RegularExpression("[a-zA-Z0-9]{,30}", ErrorMessage = "Tylko litery lub liczby!")]
        public string name { get; set; }

        [DisplayName("ID")]
        public int idGroup { get; set; }
    }
}