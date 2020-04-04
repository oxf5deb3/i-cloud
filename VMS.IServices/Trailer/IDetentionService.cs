using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Trailer;
using VMS.Model;

namespace VMS.IServices
{
    public interface  IDetentionService : IService
    {
        /// <summary>
        /// 查询扣押
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<t_detention> QueryDetention(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count);

        /// <summary>
        /// 添加扣押记录
        /// </summary>
        /// <param name="trailerDTO"></param>
        /// <returns></returns>
        bool AddDetention(DetentionDTO detentionDTO);

        /// <summary>
        /// 更新扣押记录
        /// </summary>
        /// <param name="trailerDTO"></param>
        /// <returns></returns>
        bool UpdateDetention(DetentionDTO detentionDTO);


        /// <summary>
        /// 删除扣押记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteDetention(string id);
    }
}
