using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    [MetadataType(typeof(AspNetUsersExtension))]
    partial class AspNetUsers
    {
    }

    public class AspNetUsersExtension
    {
        [DisplayName("Nazwa Użytkownika")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Grupa")]
        public int Group { get; set; }

        [DisplayName("Data utworzenia")]
        public DateTime creationDate { get; set; }

    }
}