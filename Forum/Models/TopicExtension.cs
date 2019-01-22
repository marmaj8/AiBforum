using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    [MetadataType(typeof(TopicExtension))]
    partial class Topic
    {
    }
    public class TopicExtension
    {
        [DisplayName("Topic Name")]
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must have 2-100 chars")]
        public string name { get; set; }

        [DisplayName("Section")]
        [Required(ErrorMessage = "Please select Section")]
        public int sectionFK { get; set; }

        [DisplayName("Id")]
        public int idTopic { get; set; }
    }
}