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
        [DisplayName("Nazwa tematu")]
        [Required(ErrorMessage = "Proszę podać nazwę")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "NAzwa musi się składać z 2-100 znaków")]
        public string name { get; set; }

        [DisplayName("Seekcja")]
        [Required(ErrorMessage = "Proszę wybrać sekcję")]
        public int sectionFK { get; set; }

        [DisplayName("ID")]
        public int idTopic { get; set; }
    }
}