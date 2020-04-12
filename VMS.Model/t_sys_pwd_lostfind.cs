﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_sys_pwd_lostfind:BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //是主键, 还是标识列
        public decimal id{get;set;}
        public string user_id{get;set;}
        public string email{get;set;}
        public string guid{get;set;}
        public DateTime create_date{get;set;}
        public DateTime valid_date {get;set;}
        public DateTime modify_date {get;set;}
        public string status{get;set;}
        public string memo{get;set;}

    }
}
