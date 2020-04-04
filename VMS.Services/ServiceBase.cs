using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using VMS.DAL;
using VMS.DTO;
using VMS.Model;
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

        #region 打印模板
        public virtual bool AddPrintTemplate(PrintTemplateDTO dto)
        {
            var sb = new StringBuilder();
            sb.Append("insert into t_sys_print_template(status,type,oper_id,name,html)");
            sb.Append(" values(@status,@type,@oper_id,@name,@html)");
            SqlParam[] sqlparams = new SqlParam[] {
                new SqlParam("@status",DbType.String,dto.status),
                new SqlParam("@type",DbType.Int16,dto.type),
                new SqlParam("@oper_id",DbType.String,dto.oper_id),
                new SqlParam("@name",DbType.String,dto.name),
                new SqlParam("@html",DbType.String,dto.html),
            };
            return DbContext.ExecuteBySql(sb, sqlparams) > 0;
        }

        public bool DeletePrintTemplate(PrintTemplateDTO dto)
        {
            var sb = new StringBuilder();
            sb.Append(" delete from t_sys_print_template where status='1' and id=@id");
            SqlParam[] sqlparams = new SqlParam[] {
                new SqlParam("@id",DbType.Decimal,dto.id)
            };
            return DbContext.ExecuteBySql(sb, sqlparams) > 0;
        }

        public virtual bool UpdatePrintTemaplte(PrintTemplateDTO dto)
        {

            var sb = new StringBuilder();
            sb.Append(" update t_sys_print_template set ");
            sb.Append(" html=@html");
            sb.Append(" where id=@id");
            SqlParam[] sqlparams = new SqlParam[] {
                new SqlParam("@id",DbType.Decimal,dto.id),
                new SqlParam("@html",DbType.String,dto.html)
            };
            return DbContext.ExecuteBySql(sb, sqlparams) > 0;
        }

        public virtual List<PrintTemplateDTO> LoadTemplateByOperId(int type, string oper_id)
        {
            var sb = new StringBuilder();
            sb.Append(" select id,status,type,oper_id,name,html,(case type when 0 then '1' when 1 then '0' end) as selected ");
            sb.Append(" from t_sys_print_template ");
            sb.Append(" where type=@type and oper_id=@oper_id");
            SqlParam[] sqlparams = new SqlParam[] {
                new SqlParam("@type",DbType.Int16,type),
                new SqlParam("@oper_id",DbType.String,oper_id)
            };
            return (List<PrintTemplateDTO>)DbContext.GetDataListBySQL<PrintTemplateDTO>(sb, sqlparams);
        }
        #endregion

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
