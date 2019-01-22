using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    [MetadataType(typeof(AccountExtension))]
    partial class Account
    {
    }
    public class AccountExtension
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name must have 2-30 chars")]
        [RegularExpression("[a-zA-Z0-9]{2,30}", ErrorMessage = "Only letters or numbers")]
        public string name { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(100, ErrorMessage = "Less than 100 chars")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "This is not an email address")]
        public string mail { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Passwrod must contain 4-30 chars")]
        public string pass { get; set; }

        [DisplayName("Group")]
        [Required(ErrorMessage = "Please select Group")]
        public int groupFK { get; set; }

        [DisplayName("Id")]
        public int idAccount { get; set; }

        [DisplayName("Creation Date")]
        public Nullable<System.DateTime> creationDate { get; set; }
    }
}