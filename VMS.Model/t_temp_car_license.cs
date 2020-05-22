using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_temp_car_license
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public decimal id { get; set; }
        public string id_no { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public DateTime birthday { get; set; }
        public string folk { get; set; }
        public string now_addr { get; set; }
        public string old_addr { get; set; }
        public string region_no { get; set; }
        public string permitted_card_type_no { get; set; }
        public string check_man { get; set; }
        public DateTime check_date { get; set; }
        public string nation_no { get; set; }
        public string status { get; set; }
        public string oper_date { get; set; }
        public string oper_id { get; set; }
        public string modify_date { get; set; }
        public string modify_oper_id { get; set; }
        public byte[] time_stamp { get; set; }
        public string user_photo_path { get; set; }
        public string car_1_img_path { get; set; }
        public string car_2_img_path { get; set; }
        public string engine_no_img_path { get; set; }
        public string vin_no_img_path { get; set; }
        public string img4_url { get; set; }
        public string img5_url { get; set; }
        public string car_type { get; set; }
        public string temp_number { get; set; }
        public string engine_no { get; set; }
        public string vin { get; set; }
        public string passenger { get; set; }
        public string cargo { get; set; }
        public string label_type { get; set; }
        public string id_card { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string addr { get; set; }
        public string car_color { get; set; }
        public string phone { get; set; }
        public string work_unit { get; set; }
    }
}
