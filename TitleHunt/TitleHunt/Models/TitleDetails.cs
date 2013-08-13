using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TitleHunt.Models
{
    public class TitleDetails
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public int? ReleaseYear { get; set; }
        public string GenreName { get; set; }
        public string Description { get; set; }
        public string Participant { get; set; }
        public string Role { get; set; }
        
    }
}