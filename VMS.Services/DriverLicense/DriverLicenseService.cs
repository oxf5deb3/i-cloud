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

namespace VMS.Services
{
    public class DriverLicenseService : BaseReportService, IDriverLicenseService
    {

        public const string BASE_PATH = "C:\\存储\\资料\\"; 

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

        public bool QueryTemporaryDrivingLicense(string name)
        {
            var findSql = new StringBuilder();
            bool flag=false;
            findSql.AppendFormat("select * from t_temp_driver_license where 1=1");
            findSql.AppendFormat("and name like '%{0}%'",name);
            var lst = DbContext.GetDataListBySQL<DriverLicenseDTO>(findSql);
            if (lst.Count > 0)
            {
                return true;
            }
            return flag;
        }

        public bool AddTemporaryDrivingLicense(TemporaryDriverLicenseDTO driverLicenseDTO)
        {

            String photoBase64 = driverLicenseDTO.user_photo_base64.Substring(driverLicenseDTO.user_photo_base64.IndexOf(",")+1);

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

            String chejiaPhotoPath =BASE_PATH+FileUtils.generateFileName(temporaryDrivingPermitDTO.name,
                temporaryDrivingPermitDTO.id_no, FileUtils._certificateType_LSXS, FileUtils.PHOTO_TYPE_chejia);



            FileUtils.Base64ToFileAndSave(userInfoPhotoBase64, userInfoPhotoPath);//创建人员信息照
            FileUtils.Base64ToFileAndSave(car_1_value_base64, car_1_PhotoPath);//创建车辆信息照1

            FileUtils.Base64ToFileAndSave(car_2_value_base64, car_2_PhotoPath);//创建车辆信息照2

            FileUtils.Base64ToFileAndSave(engine_no_value_base64, fadongjiPhotoPath);//创建发动机号照

            FileUtils.Base64ToFileAndSave(vin_no_value_base64, chejiaPhotoPath);//创建车架照



            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_temp_car_license(check_man,addr,folk,nation_no,birthday,sex,permitted_car_type_no,name,check_date,car_type,temp_number,engine_no,vin,passenger,cargo,label_type,start_date,end_date,user_photo_path,id_no,id_card,oper_id,oper_date,region_no) "
            + "values(@check_man,@addr,@folk,@nation_no,@birthday,@sex,@permitted_card_type_no,@name,@check_date,@car_type,@temp_number,@engine_no,@vin,@passenger,@cargo,@label_type,@start_date,@end_date,@user_photo_path,@id_no,@id_card,@oper_id,@oper_date,@region_no)");
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
            paramlst.Add(new SqlParam("@user_photo_path", userInfoPhotoPath));
            paramlst.Add(new SqlParam("@id_no", temporaryDrivingPermitDTO.id_no));
            paramlst.Add(new SqlParam("@id_card", temporaryDrivingPermitDTO.id_card));
            paramlst.Add(new SqlParam("@oper_id", temporaryDrivingPermitDTO.userInfo.user_id));
            paramlst.Add(new SqlParam("@oper_date", DateTime.Now.Date.ToString("yyyy-MM-dd")));

            paramlst.Add(new SqlParam("@car_1_img_path", car_1_PhotoPath));
            paramlst.Add(new SqlParam("@car_2_img_path",car_2_PhotoPath));
            paramlst.Add(new SqlParam("@engine_no_img_path", fadongjiPhotoPath));
            paramlst.Add(new SqlParam("@vin_no_img_path", chejiaPhotoPath));






            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        public bool AddDrivingPermit(DrivingPermitDTO drivingPermitDTO)
        {
            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_normal_card_license(card_owner,duty,work_unit,region_no,plate_no,brand_no,motor_no,cardframe_no,card_color,produce_date,issue_license_date) values(@card_owner,@duty,@work_unit,@region_no,@plate_no,@brand_no,@motor_no,@cardframe_no,@card_color,@produce_date,@issue_license_date)");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@card_owner", drivingPermitDTO.card_owner));
            paramlst.Add(new SqlParam("@duty ", drivingPermitDTO.duty));
            paramlst.Add(new SqlParam("@work_unit", drivingPermitDTO.work_unit));
            paramlst.Add(new SqlParam("@region_no", drivingPermitDTO.region_no));
            paramlst.Add(new SqlParam("@plate_no", drivingPermitDTO.plate_no));
            paramlst.Add(new SqlParam("@brand_no", drivingPermitDTO.brand_no));
            paramlst.Add(new SqlParam("@motor_no", drivingPermitDTO.motor_no));
            paramlst.Add(new SqlParam("@cardframe_no", drivingPermitDTO.cardframe_no));
            paramlst.Add(new SqlParam("@card_color", drivingPermitDTO.card_color));
            paramlst.Add(new SqlParam("@produce_date", drivingPermitDTO.produce_date));
            paramlst.Add(new SqlParam("@issue_license_date", drivingPermitDTO.issue_license_date));
            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }
        //public override List<DriverLicenseDTO> Query<DriverLicenseDTO>(IDictionary<string, dynamic> qcondition, bool loadAll, int pagesize, int pageindex, bool isasc, string orderby, ref int total, ref string err)
        //{
        //    return base.Query<DriverLicenseDTO>(qcondition, loadAll, pagesize, pageindex, isasc, orderby, ref  total, ref err);
        //}
    }
}
