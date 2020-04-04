using System;
using System.Collections.Generic;
using System.Text;
using VMS.DTO.Trailer;
using VMS.Model;

namespace VMS.IServices
{
    public interface ITrailerService : IService
    {
        /// <summary>
        /// 拖车查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<t_trailer> QueryTrailer(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count);

        /// <summary>
        /// 添加拖车记录
        /// </summary>
        /// <param name="trailerDTO"></param>
        /// <returns></returns>
        bool AddTrailer(TrailerDTO trailerDTO);

        /// <summary>
        /// 更新拖車记录
        /// </summary>
        /// <param name="trailerDTO"></param>
        /// <returns></returns>
        bool UpdateTrailer(TrailerDTO trailerDTO);


        /// <summary>
        /// 删除拖車记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteTrailer(string id);

        /// <summary>
        /// 查询单个拖車记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<TrailerDTO> QueryInfo(TrailerQueryDTO trailerQueryDTO);
    }
}
