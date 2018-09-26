using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.DTO;
using VMS.Model;

namespace VMS.Services
{
    public class DriverLicenseService : BaseReportService, IDriverLicenseService
    {
        public override string GetSqlString(IDictionary<string, dynamic> qcondition)
        {
            var sql = new StringBuilder();
            sql.Append("select id_No,name,sex,birthday,addr,b.region_name,c.type_name as permitted_car_type_name,");
            sql.Append("work_unit,first_get_license_date,valid_date_start,valid_date_end");
            sql.Append(" from t_normal_driver_license a");
            sql.Append(" left join t_bd_region  b on a.region_no = b.region_no");
            sql.Append(" left join t_bd_permitted_car_type c on a.permitted_card_type_no= c.type_no");
            sql.Append(" where 1=1 ");
            if (qcondition["id_no"] != null && !string.IsNullOrEmpty(qcondition["id_no"]))
            {
                sql.Append(" and a.id_No like @id_no ");
            }
            if (qcondition["name"] != null && !string.IsNullOrEmpty(qcondition["name"]))
            {
                sql.Append(" and a.name like @name ");
            }
            if (qcondition["permitted_car_type_no"] != null && !string.IsNullOrEmpty(qcondition["permitted_car_type_no"]))
            {
                sql.Append(" and a.permitted_card_type_no like @permitted_car_type_No");
            }
            return sql.ToString();
        }
        public override IList<SqlParam> GetParameters(IDictionary<string, dynamic> qcondition)
        {
            var lstParams = new List<SqlParam>();
            if(qcondition["id_no"]!=null && !string.IsNullOrEmpty(qcondition["id_no"]))
            {
                lstParams.Add(new SqlParam("@id_no","%"+qcondition["id_no"]+"%"));
            }
            if (qcondition["name"] != null && !string.IsNullOrEmpty(qcondition["name"]))
            {
                lstParams.Add(new SqlParam("@name", "%" + qcondition["name"] + "%"));
            }
            if (qcondition["permitted_car_type_no"] != null && !string.IsNullOrEmpty(qcondition["permitted_car_type_no"]))
            {
                lstParams.Add(new SqlParam("@permitted_car_type_no", qcondition["permitted_car_type_no"]));
            }


            return lstParams;
        }
        //public override List<DriverLicenseDTO> Query<DriverLicenseDTO>(IDictionary<string, dynamic> qcondition, bool loadAll, int pagesize, int pageindex, bool isasc, string orderby, ref int total, ref string err)
        //{
        //    return base.Query<DriverLicenseDTO>(qcondition, loadAll, pagesize, pageindex, isasc, orderby, ref  total, ref err);
        //}
    }
}
