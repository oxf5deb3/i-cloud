using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Utils
{
    public class BaseEnum:Attribute
    {
        public string Name { set;get; }
        public BaseEnum(string name)
        {
            Name=name;
        }
    }
}
