using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.FireControl;
using VMS.Model;

namespace VMS.IServices
{
    public interface IFireControlService:IService
    {
        /// <summary>
        /// 登记消防事故
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AddFireAccident(FireAccidentDTO dto);
        
        /// <summary>
        /// 登记消防设备
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AddFireEquipment(FireEquipmentDTO dto);

        List<t_fire_accident_records> ListAccident(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count);

        /// <summary>
        /// 修改事故基础内容
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        bool ModifyAccident(FireAccidentDTO dto);

        /// <summary>
        /// 删除条目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DelAccident(string ids);
        
        /// <summary>
        /// 修改图片路径
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool ModifyImgs(FireAccidentDTO dto);

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AppendImgs(FireAccidentDTO dto);

        List<t_fire_equipment_register> ListEquipment(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count);
        List<t_fire_equipment_register> ListEquipmentNew(IDictionary<string, dynamic> conditions, string orderby, bool isAsc, int? pageIndex, int? pageSize, ref int count, ref string err);

        /// <summary>
        /// 修改设备基础内容
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        bool ModifyEquipment(FireEquipmentDTO dto);

        /// <summary>
        /// 删除条目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DelEquipment(string ids);

        /// <summary>
        /// 修改图片路径
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool ModifyImgs(FireEquipmentDTO dto);

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AppendImgs(FireEquipmentDTO dto);
    }
}
