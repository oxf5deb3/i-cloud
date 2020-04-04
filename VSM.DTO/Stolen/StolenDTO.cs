using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.Stolen
{
   public class StolenDTO
    {
        /// <summary>
        /// id
        /// </summary>
        public int stolenId { get; set; }

        /// <summary>
        /// 受理案件
        /// </summary>
        public string acceptingCases { get; set; }

        /// <summary>
        /// 被盗大车
        /// </summary>
        public string totalStolenCarBig { get; set; }

        /// <summary>
        /// 被盗小车
        /// </summary>
        public string totalStolenCarSmall { get; set; }

        /// <summary>
        /// 被盗摩托车
        /// </summary>
        public string totalStolenCarMotorcycle { get; set; }

        /// <summary>
        /// 追回大车
        /// </summary>
        public string recoverBigCar { get; set; }

        /// <summary>
        /// 追回小车
        /// </summary>
        public string recoverSmallCar { get; set; }

        /// <summary>
        /// 追回摩托车
        /// </summary>
        public string recoverMotorcyle { get; set; }

        /// <summary>
        /// 未追回
        /// </summary>
        public string undetected { get; set; }


        /// <summary>
        /// 备份
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string createId { get; set; }
    }
}
