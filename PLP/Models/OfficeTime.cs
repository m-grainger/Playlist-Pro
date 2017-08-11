using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PLP.Models
{
    public class OfficeTime
    {
        public int ID { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Preview on Spotify")]
        public string Play { get; set; }
    }
}
