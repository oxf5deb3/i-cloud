using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_normal_car_license : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public decimal id { get; set; }
        public string id_no { get; set; }
        public string car_owner { get; set; }
        public string duty { get; set; }
        public string work_unit { get; set; }
        public string region_no { get; set; }
        public string addr { get; set; }
        public string plate_no { get; set; }
        public string car_type { get; set; }
        public string brand_no { get; set; }
        public string motor_no { get; set; }
        public string carframe_no { get; set; }
        public string car_color { get; set; }
        public DateTime? product_date { get; set; }
        public DateTime? issue_license_date { get; set; }
        public string status { get; set; }
        public DateTime? oper_date { get; set; }
        public string oper_id { get; set; }
        public DateTime? modify_date { get; set; }
        public string modify_oper_id { get; set; }
        public byte[] time_stamp { get; set; }
        public string user_photo_path { get; set; }
        public string car_1_img_path { get; set; }
        public string car_2_img_path { get; set; }
        public string engine_no_img_path { get; set; }
        public string vin_no_img_path { get; set; }
        public string img4_url { get; set; }
        public string img5_url { get; set; }
        public DateTime? start_date { get; set; }
        public string sex { get; set; }
        public string car_number { get; set; }
        public DateTime? end_date { get; set; }
        public string name { get; set; }
        public string nation { get; set; }
        public string passenger { get; set; }
        public DateTime? create_date { get; set; }
        public string phone { get; set; }
    }
}
