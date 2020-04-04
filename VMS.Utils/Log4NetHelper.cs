using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Utils
{
    public class Log4NetHelper
    {

        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");  // 这里的 loginfo 和 log4net.config 里的名字要一样
        public static void Info(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void Error(string info, Exception ex)
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(info, ex);
            }
        }
        public static void Error(string info)
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(info);
            }
        }

        public static void Debug(string info,Exception ex)
        {
            if (loginfo.IsDebugEnabled)
            {
                loginfo.Debug(info, ex);
            }
        }
    }
}
