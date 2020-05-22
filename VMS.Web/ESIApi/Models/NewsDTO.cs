using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS.ESIApi.Models
{
    public class NewsDTO
    {
        public decimal id { get; set; }
        public string title { get; set; }
        public string img_url { get; set; }
        public DateTime create_date { get; set; }

    }
}