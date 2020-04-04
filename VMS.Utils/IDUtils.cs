using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Utils
{
    public class IDUtils
    {

        public static string generateId(int i)
        {
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (i == (int)DrivingLicenseEnum.lsjz)
            {
                return "lsjs" + time;
            }

             if (i == (int)DrivingLicenseEnum.zsjz)
            {
                return "zsjs" + time;
            }

             if (i == (int)DrivingLicenseEnum.lsxsz)
            {
                return "lsxs" + time;
            }

             if (i == (int)DrivingLicenseEnum.zsxsz)
            {
                return "zsxs" + time;
            }


            return null;
        }

    }
}
