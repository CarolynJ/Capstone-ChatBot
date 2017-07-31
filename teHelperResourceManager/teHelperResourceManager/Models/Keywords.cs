using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace teHelperResourceManager.Models
{
    public class Keywords
    {
        public int KeywordId { get; set; }

        [Required]
        public string Keyword { get; set; }
    }
}