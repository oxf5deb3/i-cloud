using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Statistical;
using VMS.IServices;

namespace VMS.Services
{
    public class StatisticalService : BaseReportService,IStatisticalService
    {
        public DriverLicenseStatisticalDTO queryByJZ(String year){

            String sql = "select sum(case when  datepart(month,create_date)=1 then 1 else 0 end) as 'Jan',"
        +"sum(case when  datepart(month,create_date)=2 then 1 else 0 end) as 'Feb',"
        +"sum(case when  datepart(month,create_date)=3 then 1 else 0 end) as 'Mar',"
        +"sum(case when  datepart(month,create_date)=4 then 1 else 0 end) as 'Apr',"
        +"sum(case when  datepart(month,create_date)=5 then 1 else 0 end) as 'May',"
        +"sum(case when  datepart(month,create_date)=6 then 1 else 0 end) as 'June',"
        +"sum(case when  datepart(month,create_date)=7 then 1 else 0 end) as 'July',"
        +"sum(case when  datepart(month,create_date)=8 then 1 else 0 end) as 'Aug',"
        +"sum(case when  datepart(month,create_date)=9 then 1 else 0 end) as 'Sept',"
        +"sum(case when  datepart(month,create_date)=10 then 1 else 0 end) as 'Oct',"
        +"sum(case when  datepart(month,create_date)=11 then 1 else 0 end) as 'Nov',"
        +"sum(case when  datepart(month,create_date)=12 then 1 else 0 end) as 'Dec'"
    +" from t_normal_driver_license a"
   +"  where datepart(year,a.create_date)='"+year+"'";

            List<DriverLicenseStatisticalDTO> list =
                (List<DriverLicenseStatisticalDTO>)DbContext.GetDataListBySQL<
                DriverLicenseStatisticalDTO>(new StringBuilder(sql));

            if (list.Count>0)
            {
                return list[0];
            }

            return null;
        }


        public DriverLicenseStatisticalDTO queryByXS(String year)
        {

            String sql = "select sum(case when  datepart(month,create_date)=1 then 1 else 0 end) as 'Jan',"
        + "sum(case when  datepart(month,create_date)=2 then 1 else 0 end) as 'Feb',"
        + "sum(case when  datepart(month,create_date)=3 then 1 else 0 end) as 'Mar',"
        + "sum(case when  datepart(month,create_date)=4 then 1 else 0 end) as 'Apr',"
        + "sum(case when  datepart(month,create_date)=5 then 1 else 0 end) as 'May',"
        + "sum(case when  datepart(month,create_date)=6 then 1 else 0 end) as 'June',"
        + "sum(case when  datepart(month,create_date)=7 then 1 else 0 end) as 'July',"
        + "sum(case when  datepart(month,create_date)=8 then 1 else 0 end) as 'Aug',"
        + "sum(case when  datepart(month,create_date)=9 then 1 else 0 end) as 'Sept',"
        + "sum(case when  datepart(month,create_date)=10 then 1 else 0 end) as 'Oct',"
        + "sum(case when  datepart(month,create_date)=11 then 1 else 0 end) as 'Nov',"
        + "sum(case when  datepart(month,create_date)=12 then 1 else 0 end) as 'Dec'"
    + " from t_normal_car_license a"
   + "  where datepart(year,a.create_date)='" + year + "'";

            List<DriverLicenseStatisticalDTO> list =
                (List<DriverLicenseStatisticalDTO>)DbContext.GetDataListBySQL<
                DriverLicenseStatisticalDTO>(new StringBuilder(sql));

            if (list.Count > 0)
            {
                return list[0];
            }

            return null;
        }
    }
}
