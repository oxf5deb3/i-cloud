﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_sys_news:BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //是主键, 还是标识列
        public decimal id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string img_url { get; set; }
        public DateTime create_date { get; set; }
        public string create_id { get; set; }
    }
}
