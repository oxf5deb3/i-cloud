using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class SqlParam
    {
        public string FieldName
        {
            get;
            set;
        }

        public DbType DataType
        {
            get;
            set;
        }

        public object FieldValue
        {
            get;
            set;
        }

        public SqlParam()
        {
        }

        public SqlParam(string _FieldName, object _FieldValue)
            : this(_FieldName, DbType.AnsiString, _FieldValue)
        {
        }

        public SqlParam(string _FieldName, DbType _DbType, object _FieldValue)
        {
            this.FieldName = _FieldName;
            this.DataType = _DbType;
            this.FieldValue = _FieldValue;
        }
    }
}
