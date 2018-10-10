using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.DTO;
using VMS.Model;
using VMS.DTO.DriverLicense;
using VMS.DTO.DrivingPermit;
using VMS.Utils;
using System.Data;

namespace VMS.Services
{
    public class DriverLicenseService : BaseReportService, IDriverLicenseService
    {

        public const string BASE_PATH = "C:\\存储\\资料\\";

        public override string GetSqlString(IDictionary<string, dynamic> qcondition)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT [id]" +
      ",[id_no]" +
      ",[name]" +
     " ,[sex]" +
      ",[birthday]" +
     " ,permitted_card_type_no" +
      ",work_unit" +
      ",first_get_license_date" +
      ",valid_date_start" +
      ",valid_date_end" +
      ",status" +
      ",oper_date" +
      ",oper_id" +
      ",modify_date" +
      ",modiry_oper_id" +
      ",user_photo_path" +
      ",id_card,addr,b.region_name,c.type_name as permitted_car_type_name");
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
            if (qcondition["id_no"] != null && !string.IsNullOrEmpty(qcondition["id_no"]))
            {
                lstParams.Add(new SqlParam("@id_no", "%" + qcondition["id_no"] + "%"));
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

        public bool QueryTemporaryDrivingLicense(string name)
        {
            var findSql = new StringBuilder();
            bool flag = false;
            findSql.AppendFormat("select * from t_temp_driver_license where 1=1");
            findSql.AppendFormat("and name like '%{0}%'", name);
            var lst = DbContext.GetDataListBySQL<DriverLicenseDTO>(findSql);
            if (lst.Count > 0)
            {
                return true;
            }
            return flag;
        }

        public bool AddTemporaryDrivingLicense(TemporaryDriverLicenseDTO driverLicenseDTO)
        {

            String photoBase64 = driverLicenseDTO.user_photo_base64.Substring(driverLicenseDTO.user_photo_base64.IndexOf(",") + 1);

            String path = BASE_PATH + driverLicenseDTO.name + "_" + driverLicenseDTO.id_no + ".jpg";
            FileUtils.Base64ToFileAndSave(photoBase64, path);



            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_temp_driver_license(name,sex,birthday,nation_no,folk,now_addr,old_addr,permitted_card_type_no,check_man,check_date,start_date,end_date,region_no,oper_id,oper_date,id_no,user_photo_path)"
            + "values(@name,@sex,@birthday,@nation_no,@folk,@now_addr,@old_addr,@permitted_card_type_no,@check_man,@check_date,@start_date,@end_date,@region_no,@oper_id,@oper_date,@id_no,@user_photo_path)");
            List<SqlParam> paramlst = new List<SqlParam>();
            //paramlst.Add(new SqlParam("@id", IDUtils.generateId((int)DrivingLicenseEnum.lsjz)));

            paramlst.Add(new SqlParam("@name", driverLicenseDTO.name));
            paramlst.Add(new SqlParam("@sex", driverLicenseDTO.sex));
            paramlst.Add(new SqlParam("@birthday", driverLicenseDTO.birthday));
            paramlst.Add(new SqlParam("@nation_no", driverLicenseDTO.nation_no));
            paramlst.Add(new SqlParam("@folk", driverLicenseDTO.folk));
            paramlst.Add(new SqlParam("@now_addr", driverLicenseDTO.now_addr));
            paramlst.Add(new SqlParam("@old_addr", driverLicenseDTO.old_addr));
            paramlst.Add(new SqlParam("@permitted_card_type_no", driverLicenseDTO.permitted_card_type_no));
            paramlst.Add(new SqlParam("@check_man", driverLicenseDTO.check_man));
            paramlst.Add(new SqlParam("@check_date", driverLicenseDTO.check_date));
            paramlst.Add(new SqlParam("@start_date", driverLicenseDTO.start_date));
            paramlst.Add(new SqlParam("@end_date", driverLicenseDTO.end_date));
            paramlst.Add(new SqlParam("@region_no", driverLicenseDTO.region_no));

            paramlst.Add(new SqlParam("@oper_id", driverLicenseDTO.userInfo.user_id));
            paramlst.Add(new SqlParam("@oper_date", DateTime.Now.Date.ToString("yyyy-MM-dd")));
            paramlst.Add(new SqlParam("@id_no", driverLicenseDTO.id_no));
            paramlst.Add(new SqlParam("@user_photo_path", path));



            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        public bool AddDrivingLicense(DriverLicenseDTO driverLicenseDTO)
        {

            String photoBase64 = driverLicenseDTO.user_photo_base64.Substring(driverLicenseDTO.user_photo_base64.IndexOf(",") + 1);

            String path = BASE_PATH + driverLicenseDTO.name + "_" + driverLicenseDTO.id_no + ".jpg";
            FileUtils.Base64ToFileAndSave(photoBase64, path);

            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_normal_driver_license(name,sex,birthday,region_no,addr,work_unit,permitted_card_type_no,first_get_license_date,valid_date_start,valid_date_end,id_card,id_no,oper_id,oper_date,user_photo_path) "
           + " values(@name,@sex,@birthday,@region_no,@addr,@work_unit,@permitted_card_type_no,@first_get_license_date,@valid_date_start,@valid_date_end,@id_card,@id_no,@oper_id,@oper_date,@user_photo_path)");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@name", driverLicenseDTO.name));
            paramlst.Add(new SqlParam("@sex", driverLicenseDTO.sex));
            paramlst.Add(new SqlParam("@birthday", driverLicenseDTO.birthday));
            paramlst.Add(new SqlParam("@region_no", driverLicenseDTO.region_no));
            paramlst.Add(new SqlParam("@addr", driverLicenseDTO.addr));
            paramlst.Add(new SqlParam("@work_unit", driverLicenseDTO.work_unit));
            paramlst.Add(new SqlParam("@permitted_card_type_no", driverLicenseDTO.permitted_card_type_no));
            paramlst.Add(new SqlParam("@first_get_license_date", driverLicenseDTO.first_get_license_date));
            paramlst.Add(new SqlParam("@valid_date_start", driverLicenseDTO.valid_date_start));
            paramlst.Add(new SqlParam("@valid_date_end", driverLicenseDTO.valid_date_end));

            paramlst.Add(new SqlParam("@id_card", driverLicenseDTO.id_card));
            paramlst.Add(new SqlParam("@id_no", driverLicenseDTO.id_no));
            paramlst.Add(new SqlParam("@oper_id", driverLicenseDTO.userInfo.user_id));
            paramlst.Add(new SqlParam("@oper_date", DateTime.Now.Date.ToString("yyyy-MM-dd")));
            paramlst.Add(new SqlParam("@user_photo_path", path));







            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        public bool AddTemporaryDrivingPermit(TemporaryDrivingPermitDTO temporaryDrivingPermitDTO)
        {
            //人员信息照base64
            String userInfoPhotoBase64 = temporaryDrivingPermitDTO.user_photo_base64.
                Substring(temporaryDrivingPermitDTO.user_photo_base64.IndexOf(",") + 1);
            //车辆1base64
            String car_1_value_base64 = temporaryDrivingPermitDTO.car_1_value.
                Substring(temporaryDrivingPermitDTO.car_1_value.IndexOf(",") + 1);
            //车辆2base64
            String car_2_value_base64 = temporaryDrivingPermitDTO.car_2_value.
                Substring(temporaryDrivingPermitDTO.car_2_value.IndexOf(",") + 1);
            //发动机base64
            String engine_no_value_base64 = temporaryDrivingPermitDTO.engine_no_value.
                Substring(temporaryDrivingPermitDTO.engine_no_value.IndexOf(",") + 1);
            //车架base64
            String vin_no_value_base64 = temporaryDrivingPermitDTO.vin_no_value.
               Substring(temporaryDrivingPermitDTO.vin_no_value.IndexOf(",") + 1);

            //人员信息照文件名
            String userInfoPhotoPath = BASE_PATH + FileUtils.generateFileName(temporaryDrivingPermitDTO.name,
                temporaryDrivingPermitDTO.id_no, FileUtils._certificateType_LSXS, FileUtils.PHOTO_TYPE_USERINFO);

            String car_1_PhotoPath = BASE_PATH + FileUtils.generateFileName(temporaryDrivingPermitDTO.name,
                temporaryDrivingPermitDTO.id_no, FileUtils._certificateType_LSXS, FileUtils.PHOTO_TYPE_car_1);

            String car_2_PhotoPath = BASE_PATH + FileUtils.generateFileName(temporaryDrivingPermitDTO.name,
                temporaryDrivingPermitDTO.id_no, FileUtils._certificateType_LSXS, FileUtils.PHOTO_TYPE_car_2);

            String fadongjiPhotoPath = BASE_PATH + FileUtils.generateFileName(temporaryDrivingPermitDTO.name,
                temporaryDrivingPermitDTO.id_no, FileUtils._certificateType_LSXS, FileUtils.PHOTO_TYPE_fadongji);

            String chejiaPhotoPath = BASE_PATH + FileUtils.generateFileName(temporaryDrivingPermitDTO.name,
                temporaryDrivingPermitDTO.id_no, FileUtils._certificateType_LSXS, FileUtils.PHOTO_TYPE_chejia);



            FileUtils.Base64ToFileAndSave(userInfoPhotoBase64, userInfoPhotoPath);//创建人员信息照
            FileUtils.Base64ToFileAndSave(car_1_value_base64, car_1_PhotoPath);//创建车辆信息照1

            FileUtils.Base64ToFileAndSave(car_2_value_base64, car_2_PhotoPath);//创建车辆信息照2

            FileUtils.Base64ToFileAndSave(engine_no_value_base64, fadongjiPhotoPath);//创建发动机号照

            FileUtils.Base64ToFileAndSave(vin_no_value_base64, chejiaPhotoPath);//创建车架照



            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_temp_car_license(check_man,addr,folk,nation_no,birthday,sex,permitted_car_type_no,name,check_date,car_type,temp_number,engine_no,vin,passenger,cargo,label_type,start_date,end_date,user_photo_path,id_no,id_card,oper_id,oper_date,region_no,car_1_img_path,car_2_img_path,engine_no_img_path,vin_no_img_path) "
            + "values(@check_man,@addr,@folk,@nation_no,@birthday,@sex,@permitted_card_type_no,@name,@check_date,@car_type,@temp_number,@engine_no,@vin,@passenger,@cargo,@label_type,@start_date,@end_date,@user_photo_path,@id_no,@id_card,@oper_id,@oper_date,@region_no,@car_1_img_path,@car_2_img_path,@engine_no_img_path,@vin_no_img_path)");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@check_man", temporaryDrivingPermitDTO.check_man));
            paramlst.Add(new SqlParam("@sex", temporaryDrivingPermitDTO.sex));
            paramlst.Add(new SqlParam("@birthday", temporaryDrivingPermitDTO.birthday));
            paramlst.Add(new SqlParam("@folk", temporaryDrivingPermitDTO.folk));
            paramlst.Add(new SqlParam("@nation_no", temporaryDrivingPermitDTO.nation_no));
            paramlst.Add(new SqlParam("@addr", temporaryDrivingPermitDTO.addr));
            paramlst.Add(new SqlParam("@permitted_card_type_no", temporaryDrivingPermitDTO.permitted_card_type_no));
            paramlst.Add(new SqlParam("@name", temporaryDrivingPermitDTO.name));
            paramlst.Add(new SqlParam("@check_date", temporaryDrivingPermitDTO.check_date));

            paramlst.Add(new SqlParam("@region_no", temporaryDrivingPermitDTO.region_no));


            paramlst.Add(new SqlParam("@car_type", temporaryDrivingPermitDTO.car_type));
            paramlst.Add(new SqlParam("@temp_number", temporaryDrivingPermitDTO.temp_number));
            paramlst.Add(new SqlParam("@engine_no", temporaryDrivingPermitDTO.engine_no));
            paramlst.Add(new SqlParam("@vin", temporaryDrivingPermitDTO.vin));
            paramlst.Add(new SqlParam("@passenger", temporaryDrivingPermitDTO.passenger));
            paramlst.Add(new SqlParam("@cargo", temporaryDrivingPermitDTO.cargo));
            paramlst.Add(new SqlParam("@label_type", temporaryDrivingPermitDTO.label_type));

            paramlst.Add(new SqlParam("@start_date", temporaryDrivingPermitDTO.start_date));
            paramlst.Add(new SqlParam("@end_date", temporaryDrivingPermitDTO.end_date));
            paramlst.Add(new SqlParam("@id_no", temporaryDrivingPermitDTO.id_no));
            paramlst.Add(new SqlParam("@id_card", temporaryDrivingPermitDTO.id_card));
            paramlst.Add(new SqlParam("@oper_id", temporaryDrivingPermitDTO.userInfo.user_id));
            paramlst.Add(new SqlParam("@oper_date", DateTime.Now.Date.ToString("yyyy-MM-dd")));


            paramlst.Add(new SqlParam("@user_photo_path", userInfoPhotoPath));
            paramlst.Add(new SqlParam("@car_1_img_path", car_1_PhotoPath));
            paramlst.Add(new SqlParam("@car_2_img_path", car_2_PhotoPath));
            paramlst.Add(new SqlParam("@engine_no_img_path", fadongjiPhotoPath));
            paramlst.Add(new SqlParam("@vin_no_img_path", chejiaPhotoPath));






            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        public bool AddDrivingPermit(DrivingPermitDTO drivingPermitDTO)
        {

            //人员信息照base64
            String userInfoPhotoBase64 = drivingPermitDTO.user_photo_base64.
                Substring(drivingPermitDTO.user_photo_base64.IndexOf(",") + 1);
            //车辆1base64
            String car_1_value_base64 = drivingPermitDTO.car_1_value.
                Substring(drivingPermitDTO.car_1_value.IndexOf(",") + 1);
            //车辆2base64
            String car_2_value_base64 = drivingPermitDTO.car_2_value.
                Substring(drivingPermitDTO.car_2_value.IndexOf(",") + 1);
            //发动机base64
            String engine_no_value_base64 = drivingPermitDTO.engine_no_value.
                Substring(drivingPermitDTO.engine_no_value.IndexOf(",") + 1);
            //车架base64
            String vin_no_value_base64 = drivingPermitDTO.vin_no_value.
               Substring(drivingPermitDTO.vin_no_value.IndexOf(",") + 1);

            //人员信息照文件名
            String userInfoPhotoPath = BASE_PATH + FileUtils.generateFileName(drivingPermitDTO.name,
                drivingPermitDTO.id_no, FileUtils.certificateType_ZSXS, FileUtils.PHOTO_TYPE_USERINFO);

            String car_1_PhotoPath = BASE_PATH + FileUtils.generateFileName(drivingPermitDTO.name,
                drivingPermitDTO.id_no, FileUtils.certificateType_ZSXS, FileUtils.PHOTO_TYPE_car_1);

            String car_2_PhotoPath = BASE_PATH + FileUtils.generateFileName(drivingPermitDTO.name,
                drivingPermitDTO.id_no, FileUtils.certificateType_ZSXS, FileUtils.PHOTO_TYPE_car_2);

            String fadongjiPhotoPath = BASE_PATH + FileUtils.generateFileName(drivingPermitDTO.name,
                drivingPermitDTO.id_no, FileUtils.certificateType_ZSXS, FileUtils.PHOTO_TYPE_fadongji);

            String chejiaPhotoPath = BASE_PATH + FileUtils.generateFileName(drivingPermitDTO.name,
                drivingPermitDTO.id_no, FileUtils.certificateType_ZSXS, FileUtils.PHOTO_TYPE_chejia);



            FileUtils.Base64ToFileAndSave(userInfoPhotoBase64, userInfoPhotoPath);//创建人员信息照
            FileUtils.Base64ToFileAndSave(car_1_value_base64, car_1_PhotoPath);//创建车辆信息照1

            FileUtils.Base64ToFileAndSave(car_2_value_base64, car_2_PhotoPath);//创建车辆信息照2

            FileUtils.Base64ToFileAndSave(engine_no_value_base64, fadongjiPhotoPath);//创建发动机号照

            FileUtils.Base64ToFileAndSave(vin_no_value_base64, chejiaPhotoPath);//创建车架照



            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_normal_car_license(car_owner,duty,work_unit,region_no,plate_no,brand_no,motor_no,carframe_no,car_color,product_date,issue_license_date," +
            "addr,car_number,car_type,end_date,id_no,name,nation,passenger,sex,start_date,oper_id,oper_date,car_1_img_path,car_2_img_path,engine_no_img_path,vin_no_img_path)"
            + "values(@car_owner,@duty,@work_unit,@region_no,@plate_no,@brand_no,@motor_no,@carframe_no,@car_color,@product_date,@issue_license_date"
            + ",@addr,@car_number,@car_type,@end_date,@id_no,@name,@nation,@passenger,@sex,@start_date,@oper_id,@oper_date,@car_1_img_path,@car_2_img_path,@engine_no_img_path,@vin_no_img_path)");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@car_owner", drivingPermitDTO.card_owner));
            paramlst.Add(new SqlParam("@duty ", drivingPermitDTO.duty));
            paramlst.Add(new SqlParam("@work_unit", drivingPermitDTO.work_unit));
            paramlst.Add(new SqlParam("@region_no", drivingPermitDTO.region_no));
            paramlst.Add(new SqlParam("@plate_no", drivingPermitDTO.plate_no));
            paramlst.Add(new SqlParam("@brand_no", drivingPermitDTO.brand_no));
            paramlst.Add(new SqlParam("@motor_no", drivingPermitDTO.motor_no));
            paramlst.Add(new SqlParam("@carframe_no", drivingPermitDTO.carframe_no));
            paramlst.Add(new SqlParam("@car_color", drivingPermitDTO.car_color));
            paramlst.Add(new SqlParam("@product_date", drivingPermitDTO.produce_date));
            paramlst.Add(new SqlParam("@issue_license_date", drivingPermitDTO.issue_license_date));

            paramlst.Add(new SqlParam("@addr", drivingPermitDTO.addr));
            paramlst.Add(new SqlParam("@car_number", drivingPermitDTO.car_number));

            paramlst.Add(new SqlParam("@car_type", drivingPermitDTO.car_type));

            paramlst.Add(new SqlParam("@end_date", drivingPermitDTO.end_date));
            paramlst.Add(new SqlParam("@id_no", drivingPermitDTO.id_no));
            paramlst.Add(new SqlParam("@name", drivingPermitDTO.name));
            paramlst.Add(new SqlParam("@nation", drivingPermitDTO.nation));
            paramlst.Add(new SqlParam("@passenger", drivingPermitDTO.passenger));
            paramlst.Add(new SqlParam("@sex", drivingPermitDTO.sex));

            paramlst.Add(new SqlParam("@start_date", drivingPermitDTO.start_date));
            paramlst.Add(new SqlParam("@oper_id", drivingPermitDTO.userInfo.user_id));
            paramlst.Add(new SqlParam("@oper_date", DateTime.Now.Date.ToString("yyyy-MM-dd")));






            paramlst.Add(new SqlParam("@user_photo_path", userInfoPhotoPath));
            paramlst.Add(new SqlParam("@car_1_img_path", car_1_PhotoPath));
            paramlst.Add(new SqlParam("@car_2_img_path", car_2_PhotoPath));
            paramlst.Add(new SqlParam("@engine_no_img_path", fadongjiPhotoPath));
            paramlst.Add(new SqlParam("@vin_no_img_path", chejiaPhotoPath));


            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }
        //public override List<DriverLicenseDTO> Query<DriverLicenseDTO>(IDictionary<string, dynamic> qcondition, bool loadAll, int pagesize, int pageindex, bool isasc, string orderby, ref int total, ref string err)
        //{
        //    return base.Query<DriverLicenseDTO>(qcondition, loadAll, pagesize, pageindex, isasc, orderby, ref  total, ref err);
        //}

        public List<TemporaryDriverLicenseDTO> queryTemporaryDrivingLicense(int index, int pageSize, TemporaryDriverLicenseDTO data)
        {
            var start = ((index) == 0 ? 1 : index - 1) * (pageSize) + 1;
            var end = (index - 1) * pageSize + 1;

            String param = "";

            if (StringUtils.isNull(data.id_no))
            {
                param += " and a.id_no= '" + data.id_no + "'";
            }

            if (StringUtils.isNull(data.permitted_card_type_no))
            {
                param += " and a.permitted_card_type_no= '" + data.permitted_card_type_no + "'";
            }

            if (StringUtils.isNull(data.name))
            {
                param += " and a.name like " + StringUtils.FuzzyQueryAppend(data.name);
            }

            string sql = "select top " + pageSize + " oo.* from " +
"(select row_number() over(order by id_no) as rownumber,COUNT(1) OVER() AS TotalCount,* from(select [id]"
     + " ,[name]"
     + " ,[sex]"
     + " ,[birthday]"
     + " ,[folk]"
     + " ,[now_addr]"
      + ",[old_addr]"
     + " ,[permitted_card_type_no]"
     + " ,[check_man]"
     + " ,[check_date]"
     + " ,[nation_no]"
     + " ,[status]"
     + " ,[oper_date]"
     + " ,[oper_id]"
     + " ,[modify_date]"
     + " ,[modify_oper_id]"
     + " ,[time_stamp]"
      + ",[start_date]"
     + " ,[end_date]"
      + ",[id_no]"
      + ",[ck_ret]"
      + ",[user_photo_path],b.region_name,c.type_name as permitted_car_type_name from t_temp_driver_license a left join t_bd_region  b on a.region_no = b.region_no left join t_bd_permitted_car_type c on a.permitted_card_type_no= c.type_no  "
      + "where 1=1 " + param
     + " ) as o)as oo where rownumber>=" + end;
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@id_no", StringUtils.convertNull(data.id_no)));//data.id_no));
            paramlst.Add(new SqlParam("@name ", "%" + StringUtils.convertNull(data.name) + "%"));// data.name));
            paramlst.Add(new SqlParam("@type_no", StringUtils.convertNull(data.permitted_card_type_no)));//data.permitted_card_type_no));
            var querySql = new StringBuilder(sql);


            return (List<TemporaryDriverLicenseDTO>)DbContext.GetDataListBySQL<TemporaryDriverLicenseDTO>(querySql);

        }


        public List<DrivingPermitDTO> queryDrivingPermitByPage(int index, int pageSize, DrivingPermitDTO data)
        {

            String param = "";

            if (StringUtils.isNull(data.id_no))
            {
                param += " and a.id_no ='" + data.id_no + "'";
            }

            if (StringUtils.isNull(data.name))
            {
                param += " and a.name like" + StringUtils.FuzzyQueryAppend(data.name);
            }

            if (StringUtils.isNull(data.car_number))
            {
                param += " and a.name like" + StringUtils.FuzzyQueryAppend(data.car_number);
            }

            String sql = "select top "+pageSize+" o.* from (select row_number() over(order by id) as rownumber,COUNT(1) OVER() AS TotalCount,* from("
        + "SELECT [id] ,[id_no],[car_owner] ,[duty],[work_unit],[addr],[plate_no] ,[car_type]"
     + " ,[brand_no],[motor_no],[carframe_no],[car_color],[product_date],[issue_license_date]"
      + ",[status],[oper_date] ,[oper_id],[modify_date],[modify_oper_id] ,[time_stamp]"
      + ",[user_photo_path],[car_1_img_path],[car_2_img_path],[engine_no_img_path],[vin_no_img_path]"
     + ",[start_date],[sex],[car_number],[end_date] ,[name],[nation],[passenger],b.region_name "
  + "FROM [VehicleManagementSystem].[dbo].[t_normal_car_license] a "
  + "left join t_bd_region b on a.region_no=b.region_no"
  + " where 1=1 " + param
    + ")as oo) as o where rownumber>=" + ((index - 1) * pageSize + 1)+";";

            var querySql = new StringBuilder(sql);



            return (List<DrivingPermitDTO>)DbContext.GetDataListBySQL<DrivingPermitDTO>(querySql);
        }


        public List<TemporaryDrivingPermitDTO> queryTemporaryDrivingByPage(int index, int pageSize, TemporaryDrivingPermitDTO data)
        {

            String param = "";

            if (StringUtils.isNull(data.id_no))
            {
                param += " and a.id_no ='" + data.id_no + "'";
            }

            if (StringUtils.isNull(data.name))
            {
                param += " and a.name like" + StringUtils.FuzzyQueryAppend(data.name);
            }

            if (StringUtils.isNull(data.permitted_card_type_no))
            {
                param += " and a.name like" + StringUtils.FuzzyQueryAppend(data.permitted_card_type_no);
            }

            String sql = "select top "+pageSize+" o.* from (select row_number() over(order by id) as rownumber,COUNT(1) OVER() AS TotalCount,* from(SELECT [id],[name],[sex],[birthday],[folk],[now_addr],[old_addr],b.region_name "
     +" ,[permitted_car_type_no],[check_man],[check_date],[nation_no],[status],[oper_date] "
	 +"  ,[oper_id],[modify_date],[modify_oper_id],[time_stamp],[user_photo_path],[car_1_img_path] "
     +"  ,[car_2_img_path],[engine_no_img_path],[vin_no_img_path],[img4_url],[img5_url],[car_type] " 
      +" ,[temp_number],[engine_no],[vin],[passenger],[cargo],[label_type],[id_no],[id_card] "
     + "  ,[start_date],[end_date],[addr],c.type_name as permitted_card_type_name "
  +" FROM [VehicleManagementSystem].[dbo].[t_temp_car_license] a  "
  +" left join t_bd_region b on a.region_no=b.region_no "
  +" left join t_bd_permitted_car_type c on a.permitted_car_type_no=c.type_no "
   +" where 1=1  "
   + " )as oo) as o where rownumber>=" + ((index - 1) * pageSize + 1) + ";";



            var querySql = new StringBuilder(sql);



            return (List<TemporaryDrivingPermitDTO>)DbContext.GetDataListBySQL<TemporaryDrivingPermitDTO>(querySql);
        }

        /**
         * 临时行驶证校验
         * */
        public BaseResponseDTO validataTemp(String temp_car_number, String engine_no, String car_frame_no,String id_card)
        {
            BaseResponseDTO ret = new BaseResponseDTO();

            if(isExistTmpCarNumber(temp_car_number)){
                ret.message = "数据库中以存在临时车牌为:" + temp_car_number + "的号码!";
                ret.success = false;
                return ret;
            }
            if (isExistEngineNo(engine_no))
            {
                ret.message = "数据库中以存在发动机号为:" + engine_no + "的号码!";
                ret.success = false;
                return ret;
            }
            if (isExistTempVin(car_frame_no))
            {
                ret.message = "数据库中以存在车架号为:" + car_frame_no + "的号码!";
                ret.success = false;
                return ret;
            }

            if (isExistTempIdCard(id_card))
            {
                ret.message = "数据库中以存在身份证号为:" + id_card + "的号码!";
                ret.success = false;
                return ret;
            }

            
            return ret;

        }

        /**
         * 是否存在临时车牌号
         * */
        private Boolean isExistTmpCarNumber(String temp_car_number)
        {
            var tableName = "t_temp_car_license";
            var pkName = "temp_number";
            var pkVal = temp_car_number;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

        /**
       * 是否存在临时行驶证中的车架号码
       * */
        private Boolean isExistTempVin(String vin_no)
        {
            var tableName = "t_temp_car_license";
            var pkName = "vin";
            var pkVal = vin_no;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

        /**
         * 检查是否存在临时行驶证的发动机号
         * */
        private Boolean isExistEngineNo(String engine_no)
        {
            var tableName = "t_temp_car_license";
            var pkName = "engine_no";
            var pkVal = engine_no;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

        private Boolean isExistTempIdCard(String id_card)
        {
            var tableName = "t_temp_car_license";
            var pkName = "id_card";
            var pkVal = id_card;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

        /**
         * 
         * 
         * 行驶证校验
         * */
        public BaseResponseDTO validata(String car_number, String engine_no, String car_frame_no)
        {
            BaseResponseDTO ret = new BaseResponseDTO();

            if (isExistCarNumber(car_number))
            {
                ret.message = "数据库中以存在车牌为:" + car_number + "的号码!";
                ret.success = false;
                return ret;
            }
            if (isExistMotorNo(engine_no))
            {
                ret.message = "数据库中以存在发动机号为:" + engine_no + "的号码!";
                ret.success = false;
                return ret;
            }
            if (isExistCarFrameNo(car_frame_no))
            {
                ret.message = "数据库中以存在车架号为:" + car_frame_no + "的号码!";
                ret.success = false;
                return ret;
            }
            return ret;

        }

        private Boolean isExistMotorNo(String motor_no)
        {
            var tableName = "t_normal_car_license";
            var pkName = "motor_no";
            var pkVal = motor_no;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

        private Boolean isExistCarNumber(String carNumber)
        {
            var tableName = "t_normal_car_license";
            var pkName = "car_number";
            var pkVal = carNumber;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

        private Boolean isExistCarFrameNo(String CarFrameNo)
        {
            var tableName = "t_normal_car_license";
            var pkName = "carframe_no";
            var pkVal = CarFrameNo;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }


        public BaseResponseDTO validataByDriverLicense(String id_card)
        {
            BaseResponseDTO ret = new BaseResponseDTO();
            if (isExistIdCardByDriverLicense(id_card))
            {
                ret.success = false;
                ret.message = "数据库中已存在身份证号为:" + id_card + "的驾照!";
                return ret;
            }

            return ret;
        }

        /**
         * 校验驾驶证表中是否存在相同的身份证
         * */
        private Boolean isExistIdCardByDriverLicense(String id_card)
        {
            var tableName = "t_normal_driver_license";
            var pkName = "id_card";
            var pkVal = id_card;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

    }
}
