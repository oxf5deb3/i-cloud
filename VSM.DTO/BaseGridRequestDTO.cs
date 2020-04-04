using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class BaseGridRequestDTO
    {
        public BaseGridRequestDTO() { }
        public int page { get; set; }

        public int rows { get; set; }
    }
}
