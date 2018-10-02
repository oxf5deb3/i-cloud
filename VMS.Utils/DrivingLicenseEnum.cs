using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Utils
{
    public enum DrivingLicenseEnum
    {
        [BaseEnum("临时驾照")]
        lsjz=0,
        [BaseEnum("正式驾照")]
        zsjz=1,
        [BaseEnum("临时行驶证")]
        lsxsz=2,
        [BaseEnum("正式行驶证")]
        zsxsz=3,

    }
}
