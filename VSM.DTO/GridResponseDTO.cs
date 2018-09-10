using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class GridResponseDTO<T>:BaseResponseDTO
    {
        public GridResponseDTO()
        {
            rows = new List<T>();
        }
        public List<T> rows { get; set; }
        public int total { get; set; }
    }
}
