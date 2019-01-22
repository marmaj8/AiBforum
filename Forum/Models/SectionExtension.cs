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
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must have 2-100 chars")]
        [RegularExpression("[a-zA-Z0-9 ]{2,30}", ErrorMessage = "Only letters or numbers")]
        public string name { get; set; }

        [DisplayName("Group allowed to Read")]
        [Required(ErrorMessage = "Please select Group")]
        public int readGroupFk { get; set; }

        [DisplayName("Group allowed to Write")]
        [Required(ErrorMessage = "Please select Group")]
        public int writeGroupFk { get; set; }

        [DisplayName("Id")]
        public int idSection { get; set; }
    }
}