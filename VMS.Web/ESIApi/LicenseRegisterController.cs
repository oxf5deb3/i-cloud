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
    /// 注册中心 驾驶证，行驶证，临时驾驶证，临时行驶证，用户注册
    /// </summary>
    public class LicenseRegisterController : BaseApiController
    {
        /// <summary>
        /// 新增正式驾驶证
        /// </summary>
        /// <param name="token">string</param>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="birthday">出生日期</param>
        /// <param name="region_no">地区</param>
        /// <param name="addr">住址</param>
        /// <param name="work_unit">单位</param>
        /// <param name="permitted_card_type_no">准驾车型</param>
        /// <param name="first_get_license_date">初次领证日期</param>
        /// <param name="valid_date_start">有效日期起</param>
        /// <param name="valid_date_end">有效日期止</param>
        /// <param name="id_card">身份证</param>
        /// <param name="id_no">驾驶证编号</param>
        /// <param name="user_photo_base64">人像base64</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
       public BaseESIReponseDTO AddDriverLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();
            try
            {
                var dtoData = data.ToObject<DriverLicenseDTO>();
                var token = data["token"].ToObject<string>();
                var oper = ESIAuthCheck._cache.AddOrGet(token, null) as ESIUserLoginDTO;
                dtoData.userInfo = new LoginDTO() {
                    user_id=oper.user_id,
                    user_name=oper.user_name
                };
                var service = Instance<IDriverLicenseService>.Create;
                bool res = service.AddDrivingLicense(dtoData);
                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }
        }
        /// <summary>
        /// 新增临时驾驶证
        /// </summary>
        /// <param name="token">string</param>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="birthday">出生日期</param>
        /// <param name="nation_no">国籍</param>
        /// <param name="folk">地区</param>
        /// <param name="now_addr">现住址</param>
        /// <param name="old_addr">原住址</param>
        /// <param name="permitted_card_type_no">准驾车型</param>
        /// <param name="start_date">有效期</param>
        /// <param name="end_date">有效期</param>
        /// <param name="region_no">地区</param>
        /// <param name="user_photo_base64">人像base64</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseESIReponseDTO AddLsDriverLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();
            try
            {
                var dtoData = data.ToObject<TemporaryDriverLicenseDTO>();
                var token = data["token"].ToObject<string>();
                var oper = ESIAuthCheck._cache.AddOrGet(token, null) as ESIUserLoginDTO;
                dtoData.userInfo = new LoginDTO()
                {
                    user_id = oper.user_id,
                    user_name = oper.user_name
                };
                var service = Instance<IDriverLicenseService>.Create;
                bool res = service.AddTemporaryDrivingLicense(dtoData);
                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }
        }
        /// <summary>
        /// 新增正式行驶证
        /// </summary>
        /// <param name="token">string</param>
        /// <param name="region_no">地区</param>
        /// <param name="motor_no">发动机号</param>
        /// <param name="carframe_no">车架号</param>
        /// <param name="car_color">颜色</param>
        /// <param name="product_date">出厂日期</param>
        /// <param name="issue_license_date">发证日期</param>
        /// <param name="addr">住址</param>
        /// <param name="car_number">车牌号</param>
        /// <param name="car_type">车型</param>
        /// <param name="name">姓名</param>
        /// <param name="nation">国籍</param>
        /// <param name="passenger">核定载客</param>
        /// <param name="sex">性别</param>
        /// <param name="phone">联系方式</param>
        /// <param name="user_photo_path">人像base64</param>
        /// <param name="car_1_img_path">车辆照1</param>
        /// <param name="car_2_img_path">车辆照2</param>
        /// <param name="engine_no_img_path">发动机照</param>
        /// <param name="vin_no_img_path">车架照</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseESIReponseDTO AddDrivingLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();

            try
            {

                var dtoData = data.ToObject<CarLicenseDTO>();
                var token = data["token"].ToObject<string>();
                var oper = ESIAuthCheck._cache.AddOrGet(token, null) as ESIUserLoginDTO;
                dtoData.userInfo = new LoginDTO()
                {
                    user_id = oper.user_id,
                    user_name = oper.user_name
                };
                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddDrivingPermit(dtoData);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.code = ESIApi.StatusCode.FAIL;
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }

        }
        /// <summary>
        /// 新增临时行驶证
        /// </summary>
        /// <param name="token">string</param>
        /// <param name="check_man">填发员</param>
        /// <param name="sex">性别</param>
        /// <param name="birthday">出生日期</param>
        /// <param name="addr">住址</param>
        /// <param name="permitted_card_type_no">车型</param>
        /// <param name="name">姓名</param>
        /// <param name="region_no">地区</param>
        /// <param name="car_type">车型</param>
        /// <param name="temp_number">临时车牌号</param>
        /// <param name="engine_no">发动机号</param>
        /// <param name="vin">车架号</param>
        /// <param name="passenger">核定载客</param>
        /// <param name="cargo">string</param>
        /// <param name="label_type">string</param>
        /// <param name="start_date">有效期</param>
        /// <param name="end_date">有效期</param>
        /// <param name="id_card">身份证</param>
        /// <param name="user_photo_base64">人像base64</param>
        /// <param name="car_1_value">车辆照1</param>
        /// <param name="car_2_value">车辆照2</param>
        /// <param name="engine_no_img_path">发动机</param>
        /// <param name="vin_no_img_path">车架</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseESIReponseDTO AddLsDrivingLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();

            try
            {

                var dtoData = data.ToObject<TemporaryDrivingPermitDTO>();

                dtoData.userInfo = new LoginDTO();

                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddTemporaryDrivingPermit(dtoData);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.code = ESIApi.StatusCode.FAIL;
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }

        }
        /// <summary>
        /// 查询准驾车型
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseTResponseDTO<List<PermittedCarTypeDTO>> QueryAllPermittedCarType()
        {
            var ret = new BaseTResponseDTO<List<PermittedCarTypeDTO>>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IPermittedCarTypeService>.Create;
                var lst = obj.GetAllPermittedCarType(sb, paramlst, ref total);
                ret.data.AddRange(lst.Select(e => new PermittedCarTypeDTO() { type_no = e.type_no, type_name = e.type_name }));
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