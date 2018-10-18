using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.TrafficAccident
{
    public class TrafficAccidentDTO
    {
        public decimal id { get; set; }
        public string happenDate { get; set; }
        public string happenAddr { get; set; }
        public string firstPartyMan { get; set; }
        public string firstPartyAddr { get; set; }
        public string secondPartyMan { get; set; }
        public string secondPartyAddr { get; set; }
        public string accidentDesc { get; set; }
        public string mediationUnit { get; set; }
        public string mediationDate { get; set; }
        public string drawRecorder { get; set; }
        public string accidentMediator { get; set; }
        public string operId { get; set; }
        public string operDate { get; set; }
        public string modifyOperId { get; set; }
        public string modifyDate { get; set; }
        public string imgUrl { get; set; }

        public string duty { get; set; }

        public string dingPartyAddr { get; set; }

        public string dingPartyMan { get; set; }

        public string bingPartyAddr { get; set; }

        public string bingPartyMan { get; set; }
    }
}
