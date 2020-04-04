using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.IServices;
using VMS.Utils;
using VMS.Model;

namespace VMS.Services
{
    public class CarNumberService : BaseReportService, ICarNumberService
    {
        public bool ModifyCarNumber(DrivingPermitDTO dto) {
            var sql = new StringBuilder("update t_normal_car_license set car_number = @car_number, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
            SqlParam[] sqlParams = new SqlParam[] {
                new SqlParam("@car_number", dto.car_number),
                new SqlParam("@id", dto.id),
                new SqlParam("@modify_oper_id", dto.userInfo.user_id),
                new SqlParam("@modify_date", DateTime.Now.ToString())
            };

            return DbContext.ExecuteBySql(sql, sqlParams) > 0;
        }

        public bool ModifyTempCarNumber(TemporaryDrivingPermitDTO dto)
        {
            var sql = new StringBuilder("update t_temp_car_license set temp_number = @car_number, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
            SqlParam[] sqlParams = new SqlParam[] {
                new SqlParam("@car_number", dto.temp_number),
                new SqlParam("@id", dto.id),
                new SqlParam("@modify_oper_id", dto.userInfo.user_id),
                new SqlParam("@modify_date", DateTime.Now.ToString())
            };

            return DbContext.ExecuteBySql(sql, sqlParams) > 0;
        }

        public List<DTO.DrivingPermitDTO> queryDrivingPermitByPage(int index, int pageSize, DTO.DrivingPermitDTO data)
        {
            String param = "";

            if (StringUtils.isNull(data.car_number))
            {
                param += " and a.car_number ='" + data.car_number + "'";
            }

            if (StringUtils.isNull(data.name))
            {
                param += " and a.name like" + StringUtils.FuzzyQueryAppend(data.name);
            }

           

            String sql = "select top " + pageSize + " o.* from (select row_number() over(order by id) as rownumber,COUNT(1) OVER() AS TotalCount,* from("
        + "SELECT [id] ,[id_no],[car_owner] ,[duty],[work_unit],[addr],[plate_no] ,[car_type]"
     + " ,[brand_no],[motor_no],[carframe_no],[car_color],[product_date],[issue_license_date]"
      + ",[status],[oper_date] ,[oper_id],[modify_date],[modify_oper_id] ,[time_stamp]"
      + ",[user_photo_path],[car_1_img_path],[car_2_img_path],[engine_no_img_path],[vin_no_img_path]"
     + ",[start_date],[sex],[car_number],[end_date] ,[name],[nation],[passenger],b.region_name "
  + "FROM [dbo].[t_normal_car_license] a "
  + "left join t_bd_region b on a.region_no=b.region_no"
  + " where 1=1 " + param
    + ")as oo) as o where rownumber>=" + ((index - 1) * pageSize + 1) + ";";

            var querySql = new StringBuilder(sql);



            return (List<DrivingPermitDTO>)DbContext.GetDataListBySQL<DrivingPermitDTO>(querySql);
        }


        public List<TemporaryDrivingPermitDTO> queryTemporaryDrivingByPage(int index, int pageSize, TemporaryDrivingPermitDTO data)
        {

            String param = "";

            if (StringUtils.isNull(data.temp_number))
            {
                param += " and a.temp_number ='" + data.temp_number + "'";
            }

            if (StringUtils.isNull(data.name))
            {
                param += " and a.name like" + StringUtils.FuzzyQueryAppend(data.name);
            }


            String sql = "select top " + pageSize + " o.* from (select row_number() over(order by id) as rownumber,COUNT(1) OVER() AS TotalCount,* from(SELECT [id],[name],[sex],[birthday],[folk],[now_addr],[old_addr],b.region_name "
     + " ,[permitted_car_type_no],[check_man],[check_date],[nation_no],[status],[oper_date] "
     + "  ,[oper_id],[modify_date],[modify_oper_id],[time_stamp],[user_photo_path],[car_1_img_path] "
     + "  ,[car_2_img_path],[engine_no_img_path],[vin_no_img_path],[img4_url],[img5_url],[car_type] "
      + " ,[temp_number],[engine_no],[vin],[passenger],[cargo],[label_type],[id_no],[id_card] "
     + "  ,[start_date],[end_date],[addr],c.type_name as permitted_card_type_name "
  + " FROM [dbo].[t_temp_car_license] a  "
  + " left join t_bd_region b on a.region_no=b.region_no "
  + " left join t_bd_permitted_car_type c on a.permitted_car_type_no=c.type_no "
   + " where 1=1  "
   + " )as oo) as o where rownumber>=" + ((index - 1) * pageSize + 1) + ";";



            var querySql = new StringBuilder(sql);



            return (List<TemporaryDrivingPermitDTO>)DbContext.GetDataListBySQL<TemporaryDrivingPermitDTO>(querySql);
        }

    }
}
