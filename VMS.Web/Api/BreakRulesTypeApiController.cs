using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;

namespace VMS.Api
{
    public class BreakRulesTypeApiController : BaseApiController
    {
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

                var dto = data.ToObject<BreakRulesTypeDTO>();
                var info = new t_bd_breakrules_type() { name = dto.name, punish_desc = dto.punish_desc, memo = dto.memo };
                var service = Instance<IBreakRulesTypeService>.Create;
                ret.success = service.AddBreakRulesType(info,operInfo.user_id);
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
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

                var dto = data.ToObject<BreakRulesTypeDTO>();
                var info = new t_bd_breakrules_type() { id=dto.id??0,name= dto.name, punish_desc = dto.punish_desc, memo = dto.memo,oper_id=operInfo.user_id };
                var service = Instance<IBreakRulesTypeService>.Create;
                ret.success = service.EditBreakRulesType(info);
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
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

                var dto = data["deleted"].ToObject<List<BreakRulesTypeDTO>>();
                var listNos = dto.Select(e => e.id.ToString()).ToList();
                var service = Instance<IBreakRulesTypeService>.Create;
                ret.success = service.BatchDeleteBreakRulesType(listNos);
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        [System.Web.Mvc.HttpPost]
        public GridResponseDTO<BreakRulesTypeDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<BreakRulesTypeDTO>();
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
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IBreakRulesTypeService>.Create;
                var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, ref total);
                ret.rows.AddRange(lst.Select(e => new BreakRulesTypeDTO() {id=e.id, name = e.name, punish_desc = e.punish_desc, memo = e.memo }));
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

        [HttpPost]
        public BaseTResponseDTO<List<ComboDTO>> QueryAll([FromBody]JObject data)
        {
            var ret = new BaseTResponseDTO<List<ComboDTO>>();
            try
            {
                #region 参数检验
                //if (data == null)
                //{
                //    ret.success = false;
                //    ret.message = "无法获取到请求参数!";
                //}
                #endregion
                //var pageIndex = data["page"] == null ? 1 : data["page"].ToObject<int>();
                //var pageSize = data["rows"] == null ? 20 : data["rows"].ToObject<int>();
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IBreakRulesTypeService>.Create;
                var lst = obj.GetAllBreakRulesType(sb, paramlst,ref total);
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
