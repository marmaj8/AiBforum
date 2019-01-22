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
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name must have 1-30 chars")]
        [RegularExpression("[a-zA-Z0-9]{,30}", ErrorMessage = "Only letters or numbers")]
        public string name { get; set; }

        [DisplayName("Id")]
        public int idGroup { get; set; }
    }
}