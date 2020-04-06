using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_normal_driver_license : BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public decimal id { get; set; }
        public string id_no { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public DateTime birthday { get; set; }
        public string addr { get; set; }
        public string region_no { get; set; }
        public string permitted_card_type_no { get; set; }
        public string work_unit { get; set; }
        public string first_get_license_date { get; set; }
        public DateTime valid_date_start { get; set; }
        public DateTime valid_date_end { get; set; }
        public string status { get; set; }
        public string oper_date { get; set; }
        public string oper_id { get; set; }
        public string modify_date { get; set; }
        public string modify_oper_id { get; set; }
        public byte[] time_stamp { get; set; }
        public string img_url { get; set; }
        public string img0_url { get; set; }
        public string img1_url { get; set; }
        public string img2_url { get; set; }
        public string img3_url { get; set; }
        public string img4_url { get; set; }
        public string img5_url { get; set; }
        public string user_photo_path { get; set; }
        public string id_card { get; set; }
        public DateTime create_date { get; set; }
    }
}