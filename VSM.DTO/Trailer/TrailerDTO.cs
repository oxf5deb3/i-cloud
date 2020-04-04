using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.Trailer
{
   public class TrailerDTO
    {
        /// <summary>
        /// id
        /// </summary>
        public int trailerNo { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string trailerDate { get; set; }

        /// <summary>
        /// 人数
        /// </summary>
        public string number { get; set; }

        /// <summary>
        /// 摩托总数
        /// </summary>
        public string totalMotorcycle { get; set; }

        /// <summary>
        /// 三轮摩托
        /// </summary>
        public string tricycle { get; set; }

        /// <summary>
        /// 电动摩托
        /// </summary>
        public string batteryMotorcycle { get; set; }

        /// <summary>
        /// 车辆总数
        /// </summary>
        public string totalVehicle { get; set; }

        /// <summary>
        ///大车
        /// </summary>
        public string bigCar { get; set; }

        /// <summary>
        /// 小车
        /// </summary>
        public string smallCar { get; set; }

        /// <summary>
        /// 拖拉机
        /// </summary>
        public string tractor { get; set; }

        /// <summary>
        /// 拖车总数
        /// </summary>
        public string totalTrailer { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string createId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
