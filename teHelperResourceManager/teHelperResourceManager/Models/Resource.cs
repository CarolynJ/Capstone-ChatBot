using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace teHelperResourceManager.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }

        [Required]
        public string ResourceTitle { get; set; }

        [Required]
        public string ResourceContent { get; set; }

        [Required]
        public bool PathwayResource { get; set; }
    }
}