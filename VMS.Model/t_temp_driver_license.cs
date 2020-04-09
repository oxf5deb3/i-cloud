using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_temp_driver_license:BaseEntity
    {
       [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public decimal id{get;set;}
        public string name{get;set;}
        public string sex{get;set;}
        public DateTime birthday{get;set;}
        public string folk{get;set;}
        public string now_addr{get;set;}
        public string old_addr{get;set;}
        public string region_no{get;set;}
        public string permitted_card_type_no{get;set;}
        public string check_man{get;set;}
        public string check_date{get;set;}
        public string nation_no{get;set;}
        public string status{get;set;}
        public DateTime oper_date {get;set;}
        public string oper_id{get;set;}
        public DateTime modify_date {get;set;}
        public string modify_oper_id{get;set;}
        public byte[] time_stamp{get;set;}
        public string img_url{get;set;}
        public string img0_url{get;set;}
        public string img1_url{get;set;}
        public string img2_url{get;set;}
        public string img3_url{get;set;}
        public string img4_url{get;set;}
        public string img5_url{get;set;}
        public DateTime start_date {get;set;}
        public DateTime end_date {get;set;}
        public string id_no{get;set;}
        public string ck_ret{get;set;}
        public string user_photo_path{get;set;}
    }
}
