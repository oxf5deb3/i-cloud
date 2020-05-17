using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using VMS.DTO;
using VMS.ESIApi.Models;
using VMS.ESIApi.Utils;
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
        /// <param name="token"></param>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="age">年龄</param>
        /// <param name="phone">联系方式</param>
        /// <param name="id_card">身份证</param>
        /// <param name="car_no">车牌号</param>
        /// <param name="driver_no">驾驶证号</param>
        /// <param name="driving_no">行驶证号</param>
        /// <param name="carframe_no">车架号</param>
        /// <param name="engine_no">发动机号</param>
        /// <param name="breakrule_type_id">违章类型id</param>
        /// <param name="breakrule_addr">违章地址</param>
        /// <param name="breakrule_date">违章日期</param>
        /// <param name="memo">备注</param>
        /// <param name="memo1">备注1</param>
        /// <param name="memo2">备注2</param>
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
                    engine_no = dto.engine_no,
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
        /// <param name="token"></param>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="age">年龄</param>
        /// <param name="phone">联系方式</param>
        /// <param name="id_card">身份证</param>
        /// <param name="car_no">车牌号</param>
        /// <param name="driver_no">驾驶证号</param>
        /// <param name="driving_no">行驶证号</param>
        /// <param name="carframe_no">车架号</param>
        /// <param name="engine_no">发动机号</param>
        /// <param name="breakrule_type_id">违章类型id</param>
        /// <param name="breakrule_addr">违章地址</param>
        /// <param name="breakrule_date">违章日期</param>
        /// <param name="memo">备注</param>
        /// <param name="memo1">备注1</param>
        /// <param name="memo2">备注2</param>
        /// <param name="id"></param>
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
                var token = data["token"].ToObject<string>();
                var oper = ESIAuthCheck._cache.AddOrGet(token, null) as ESIUserLoginDTO;
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
                    engine_no = dto.engine_no,
                    car_no = dto.car_no,
                    driver_no = dto.driver_no,
                    driving_no = dto.driving_no,
                    id_card = dto.id_card,
                    oper_date = DateTime.Now,
                    oper_id = oper.user_id,// operInfo.user_id,
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
        /// <param name="deleted">[id]</param>
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
        /// <param name="page">第几页</param>
        /// <param name="rows">页大小</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">asc/desc</param>
        /// <param name="name">查询条件</param>
        /// <param name="id_card">查询条件</param>
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


        /// <summary>
        /// 查询违章类型
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseTResponseDTO<List<ComboDTO>> QueryAllBreakRuleType()
        {
            var ret = new BaseTResponseDTO<List<ComboDTO>>();
            try
            {
                #region 参数检验
             
                #endregion
                //var pageIndex = data["page"] == null ? 1 : data["page"].ToObject<int>();
                //var pageSize = data["rows"] == null ? 20 : data["rows"].ToObject<int>();
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IBreakRulesTypeService>.Create;
                var lst = obj.GetAllBreakRulesType(sb, paramlst, ref total);
                ret.data.AddRange(lst.Select(e => new ComboDTO() { id = e.id.ToString(), text = e.name }));
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