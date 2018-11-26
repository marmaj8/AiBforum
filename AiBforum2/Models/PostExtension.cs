using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AiBforum2.Models
{
    [MetadataType(typeof(PostExtension))]
    partial class Post
    {
    }
    public class PostExtension
    {
        [DisplayName("Text")]
        [Required(ErrorMessage = "Please enter Text")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Text must be shorten than 2000 chars")]
        public string text { get; set; }

        [DisplayName("Id")]
        public int idPost { get; set; }

        [DisplayName("Author")]
        public int author { get; set; }

        [DisplayName("Topic")]
        public int topic { get; set; }

        [DisplayName("Date")]
        public Nullable<System.DateTime> date { get; set; }
    }
}