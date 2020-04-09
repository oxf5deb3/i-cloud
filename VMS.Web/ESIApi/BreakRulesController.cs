using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;

namespace VMS.ESIApi
{
    /// <summary>
    /// 违章
    /// </summary>
    public class BreakRulesController : BaseApiController
    {

        /// <summary>
        /// 添加违章信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO Add([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<BreakRulesDTO>();
                var info = new t_bd_breakrules()
                {
                    name = dto.name,
                    sex = dto.sex,
                    age = dto.age,
                    breakrule_addr = dto.breakrule_addr,
                    breakrule_date = dto.breakrule_date,
                    breakrule_type_id = dto.breakrule_type_id,
                    carframe_no = dto.carframe_no,
                    car_no = dto.car_no,
                    driver_no = dto.driver_no,
                    driving_no = dto.driving_no,
                    id_card = dto.id_card,
                    oper_date = DateTime.Now,
                    oper_id = "",
                    phone = dto.phone,
                    memo1 = dto.memo1,
                    memo2 = dto.memo2,
                    memo = dto.memo
                };
                var service = Instance<IBreakRulesQueryService>.Create;
                ret.success = service.AddBreakRules(info, "");
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        /// <summary>
        /// 编辑违章信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO Edit([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<BreakRulesDTO>();
                var info = new t_bd_breakrules()
                {
                    id = dto.id ?? 0,
                    name = dto.name,
                    sex = dto.sex,
                    age = dto.age,
                    breakrule_addr = dto.breakrule_addr,
                    breakrule_date = dto.breakrule_date,
                    breakrule_type_id = dto.breakrule_type_id,
                    carframe_no = dto.carframe_no,
                    car_no = dto.car_no,
                    driver_no = dto.driver_no,
                    driving_no = dto.driving_no,
                    id_card = dto.id_card,
                    oper_date = DateTime.Now,
                    oper_id = "",// operInfo.user_id,
                    phone = dto.phone,
                    memo1 = dto.memo1,
                    memo2 = dto.memo2,
                    memo = dto.memo
                };
                var service = Instance<IBreakRulesQueryService>.Create;
                ret.success = service.EditBreakRules(info);
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        /// <summary>
        /// 删除违章信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO Del([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data["deleted"].ToObject<List<BreakRulesDTO>>();
                var listNos = dto.Select(e => e.id.ToString()).ToList();
                var service = Instance<IBreakRulesQueryService>.Create;
                ret.success = service.BatchDeleteBreakRules(listNos);
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        /// <summary>
        /// 查询违章信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public GridResponseDTO<BreakRulesDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<BreakRulesDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion
                var pageIndex = data["page"] == null ? 1 : data["page"].ToObject<int>();
                var pageSize = data["rows"] == null ? 20 : data["rows"].ToObject<int>();
                var sort = data["sort"] == null ? "id" : data["sort"].ToObject<string>();
                bool isAsc = data["order"] == null ? true : data["order"].ToObject<string>() == "asc";
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                var name = data["name"] == null ? "" : data["name"].ToObject<string>();
                var id_card = data["id_card"] == null ? "" : data["id_card"].ToObject<string>();
                if (!string.IsNullOrEmpty(name))
                {
                    sb.Append(" and r.name like @name ");
                    paramlst.Add(new SqlParam() { FieldName = "name", FieldValue = name + "%" });
                }
                if (!string.IsNullOrEmpty(id_card))
                {
                    sb.Append(" and r.id_card like @id_card ");
                    paramlst.Add(new SqlParam() { FieldName = "id_card", FieldValue = id_card + "%" });
                }

                int total = 0;
                var obj = Instance<IBreakRulesQueryService>.Create;
                var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, isAsc, sort, ref total);
                ret.rows = lst;
                ret.total = total;
                return ret;

            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
    }
}