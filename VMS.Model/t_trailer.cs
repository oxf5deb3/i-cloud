using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
   public class t_trailer
    {
        /// <summary>
        /// id
        /// </summary>
        public int trailer_no { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string trailer_date { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public string trailer_number { get; set; }

        /// <summary>
        /// 摩托总数
        /// </summary>
        public string trailer_totalmotorcycle { get; set; }

        /// <summary>
        /// 三轮摩托
        /// </summary>
        public string trailer_tricycle { get; set; }

        /// <summary>
        /// 电动摩托
        /// </summary>
        public string trailer_batterymotorcycle { get; set; }

        /// <summary>
        /// 车辆总数
        /// </summary>
        public string trailer_totalvehicle { get; set; }

        /// <summary>
        ///大车
        /// </summary>
        public string trailer_bigcar { get; set; }

        /// <summary>
        /// 小车
        /// </summary>
        public string trailer_smallcar { get; set; }

        /// <summary>
        /// 拖拉机
        /// </summary>
        public string trailer_tractor { get; set; }

        /// <summary>
        /// 拖车总数
        /// </summary>
        public string trailer_totaltrailer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
}
}
