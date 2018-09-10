using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using VMS.DAL;
using VMS.Utils;

namespace VMS.Services
{
    public class ServiceBase
    {
        #region 属性
        protected SqlServerDBHelper DbContext
        {
            get
            {
                var name = string.Format("CallContextName:{0}", this.GetHashCode());
                var dbHelper = CallContext.GetData(name) as SqlServerDBHelper;
                if (dbHelper == null)
                {
                    dbHelper = new SqlServerDBHelper(ConfigHelper.GetAppSettings("DefaultConnection"));
                    CallContext.SetData(name, dbHelper);
                }
                return dbHelper;
            }
        }
        #endregion
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
