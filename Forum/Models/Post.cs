//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Forum.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        public int idPost { get; set; }
        public string author { get; set; }
        public int topic { get; set; }
        public string text { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Topic Topic1 { get; set; }
    }
}