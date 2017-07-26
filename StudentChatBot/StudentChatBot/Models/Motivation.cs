using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentChatBot.Models
{
    [Serializable]
    public class Motivation
    {
        public int MotivationCode { get; set; }
        
        public string Quote { get; set; }

        public string QuoteSource { get; set; }

        public string ImageCode { get; set; }
    }
}