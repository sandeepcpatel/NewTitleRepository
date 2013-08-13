using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TitleHunt.Models
{
    public class TitleView
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public int? ReleaseYear { get; set; }
        public string GenreName { get; set; }
        public string Description{ get; set; }
        public string Participant { get; set; }
        public string Role { get; set; }
        public bool? IsKey { get; set; }
        public bool? IsOnScreen { get; set; }
        public string ParticipantType { get; set; }
        public int ParticipantId { get; set; }
        public bool? AwardWon { get; set; }
        public string Award { get; set; }
        public int? AwardYear { get; set; }
        public string AwardCompany { get; set; }
        
        
    }
}