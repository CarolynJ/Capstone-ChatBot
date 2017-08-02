using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentChatBot.Models.Matchmaking
{
    public class CompanyContact
    {
        public string CompanyName { get; set; }
        public List<string> Interviewers { get; set; }
        public Dictionary<string, string> MainContactInfo { get; set; }
    }
}