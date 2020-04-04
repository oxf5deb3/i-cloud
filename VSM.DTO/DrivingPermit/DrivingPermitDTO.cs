using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class DrivingPermitDTO
    {
        public decimal id { get; set; }
        public string car_owner { get; set; }

        public string modify_oper_id { get; set; }
        public string duty { get; set; }
        public string work_unit { get; set; }
        public string region_no { get; set; }
        public string plate_no { get; set; }
        public string brand_no { get; set; }
        public string produce_date { get; set; }
        public string issue_license_date { get; set; }

        public string product_date { get; set; }
        public string addr { get; set; }
        public string car_1_value { get; set; } //车辆照1
        public string car_2_value { get; set; } //车辆照2
        public string car_color { get; set; } //车辆颜色
        public string car_number { get; set; } //车牌
        public string car_type { get; set; }//车型
        public string carframe_no { get; set; }//车驾照
        public string end_date { get; set; } //结束时间
        public string engine_no_value { get; set; } //发动机照
        public string id_no { get; set; }//证件
        public string motor_no { get; set; } //发动机
        public string name { get; set; }//姓名
        public string nation { get; set; }//国籍
        public string passenger { get; set; }//载客
        public string sex { get; set; }//性别
        public string user_photo_base64 { get; set; }//用户信息照
        public string start_date { get; set; }//开始时间
        public string vin_no_value { get; set; }//车架照

        public LoginDTO userInfo { get; set; }

        public String TotalCount { get; set; }

        public string region_name { get; set; }

        public string user_photo_path { get; set; }
        public string car_1_img_path { get; set; }
        public string car_2_img_path { get; set; }
        public string engine_no_img_path { get; set; }
        public string vin_no_img_path { get; set; }

        public string phone { get; set; }

    }
}
