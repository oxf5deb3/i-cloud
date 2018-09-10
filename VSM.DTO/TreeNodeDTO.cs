using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
     public class TreeNodeDTO
    {
         public string id { get; set; }

         public string text { get; set; }

         public string state { get; set; }

         public List<TreeNodeDTO> children { get; set; }
    }
}
