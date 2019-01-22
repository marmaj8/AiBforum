using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class NewTopic
    {
        private int section;

        private string name;

        private string text;

        private int author;

        public int Section { get => section; set => section = value; }
        public string Name { get => name; set => name = value; }
        public string Text { get => text; set => text = value; }
        public int Author { get => author; set => author = value; }
    }
}