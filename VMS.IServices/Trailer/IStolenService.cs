using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Stolen;
using VMS.Model;

namespace VMS.IServices
{
    public interface IStolenService : IService
    {
        /// <summary>
        /// 扣押查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<t_stolen> QueryStolen(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count);

        /// <summary>
        /// 添加扣押记录
        /// </summary>
        /// <param name="trailerDTO"></param>
        /// <returns></returns>
        bool AddStolen(StolenDTO stolenDTO);

        /// <summary>
        /// 更新扣押记录
        /// </summary>
        /// <param name="trailerDTO"></param>
        /// <returns></returns>
        bool UpdateStolen(StolenDTO stolenDTO);


        /// <summary>
        /// 删除扣押记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteStolen(string id);
    }
}
