using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Utils
{
    public class StringUtils
    {
        public static String convertNull(string str)
        {
            if(null==str||str.Equals("null")||str.Equals("NULL")||str.Equals("")){
                return "";
            }
            else
            {
                return str;
            }
        }

        public static String FuzzyQueryAppend(String str)
        {
            if (null == str || str.Equals("null") || str.Equals("NULL") || str.Equals(""))
            {
                return "''";
            }
            else
            {
                return "'%"+str+"%'";
            }
        }

        public static bool isNull(String str)
        {
            if (null == str || str.Equals("null") || str.Equals("NULL") || str.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
