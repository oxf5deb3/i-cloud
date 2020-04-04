﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_fire_accident_records
    {
        public decimal id { get; set; }

        public DateTime happen_date { get; set; }

        public string happen_addr { get; set; }

        public string accident_desc { get; set; }
        public string out_police_cars { get; set; }
        public string out_police_mans { get; set; }

        public string process_results { get; set; }
        public string oper_id { get; set; }

        public DateTime oper_date { get; set; }

        public string modify_oper_id { get; set; }

        public DateTime modify_date { get; set; }

        public string img_url { get; set; }
        public string name { get; set; }
        public string sex { get; set; }

        public string age { get; set; }

        public string folk { get; set; }

        public string addr { get; set; }

        public string phone { get; set; }

        public string loss { get; set; }
        public string finance_loss { get; set; }

        public string casualties { get; set; }
    }
}
