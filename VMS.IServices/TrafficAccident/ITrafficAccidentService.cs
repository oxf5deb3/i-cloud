using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.TrafficAccident;
using VMS.Model;

namespace VMS.IServices
{
    public interface ITrafficAccidentService : IService
    {
        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AddTrafficAccident(TrafficAccidentDTO dto);

        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<t_accident_records> ListAccident(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        bool ModifyAccident(TrafficAccidentDTO dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DelAccident(string ids);

        /// <summary>
        /// 修改图片路径
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool ModifyImgs(TrafficAccidentDTO dto);

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AppendImgs(TrafficAccidentDTO dto);
    }
}
