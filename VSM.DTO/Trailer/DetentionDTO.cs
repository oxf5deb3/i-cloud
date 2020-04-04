using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.Trailer
{
   public class DetentionDTO
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int detentionId { get; set; }

        /// <summary>
        /// 总扣押人数
        /// </summary>
        public string totalDetention { get; set; }

        /// <summary>
        /// 总扣押人数 男
        /// </summary>
        public string totalDetentionBoy { get; set; }

        /// <summary>
        /// 总扣押人数 女
        /// </summary>
        public string totalDetentionGirl { get; set; }


        /// <summary>
        /// 已经释放
        /// </summary>
        public string alreadyRelease { get; set; }

        /// <summary>
        /// 现扣押
        /// </summary>
        public string nowDetentionTotal { get; set; }

        /// <summary>
        /// 现扣押 男
        /// </summary>
        public string nowDetentionBoy { get; set; }

        /// <summary>
        /// 现扣押 女
        /// </summary>
        public string nowDetentionGirl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string createId { get; set; }

        /// <summary>
        /// 交通事故扣押
        /// </summary>
        public string trafficAccidentDetention { get; set; }

        /// <summary>
        /// 不配合扣押
        /// </summary>
        public string notCooperateDetention { get; set; }


        /// <summary>
        /// 总共移交警察局
        /// </summary>
        public string devolvePoliceTotal { get; set; }

        /// <summary>
        /// 移交警察局男
        /// </summary>
        public string devolvePoliceBoy { get; set; }

        /// <summary>
        /// 移交警察局女
        /// </summary>
        public string devolvePoliceGirl { get; set; }

        /// <summary>
        /// 总共移交检察院人数
        /// </summary>
        public string devolveProcuratorateTotal { get; set; }

        /// <summary>
        /// 移交检察院 男人数
        /// </summary>
        public string devolveProcuratorateBoy { get; set; }


        /// <summary>
        /// 移交检察院 女人数
        /// </summary>
        public string devolveProcuratorateGirl { get; set; }
    }
}
