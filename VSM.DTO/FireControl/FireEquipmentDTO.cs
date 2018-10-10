using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.FireControl
{
    public class FireEquipmentDTO
    {
        public decimal id { get; set; }
        public string name { get; set; }

        public string model { get; set; }

        public int count { get; set; }

        public string address { get; set; }

        public string datetime { get; set; }

        public string desc { get; set; }

        public string liable { get; set; }

        public string imgs { get; set; }

        public string operId { get; set; }

        public string operDate { get; set; }

        public string modifyOperId { get; set; }

        public string modifyDate { get; set; }
    }
}
