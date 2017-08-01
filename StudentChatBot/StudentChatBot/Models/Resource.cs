using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentChatBot.Models
{
    [Serializable]
    public class Resource
    {
        public int ResourceId { get; set; }

        public string ResourceTitle { get; set; }

        public string ResourceContent { get; set; }

        public bool PathwayResource { get; set; }
    }
}