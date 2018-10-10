using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VMS.DTO.FireControl
{
    public class FireAccidentDTO
    {
        public decimal id { get; set; }
        public string address { get; set; }

        public string datetime { get; set; }

        public string desc { get; set; }

        public string cars { get; set; }

        public string names { get; set; }

        public string result { get; set; }

        public string imgs { get; set; }

        public string operId { get; set; }

        public string operDate { get; set; }

        public string modifyOperId { get; set; }

        public string modifyDate { get; set; }
    }
}
