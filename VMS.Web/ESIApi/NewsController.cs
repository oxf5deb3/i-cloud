using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VMS.DTO;
using VMS.ESIApi.Models;
using VMS.IServices;
using VMS.ServiceProvider;

namespace VMS.ESIApi
{
    /// <summary>
    /// 资讯
    /// </summary>
    public class NewsController : BaseApiController
    { 
      /// <summary>
      /// 根据id获取资讯内容
      /// </summary>
      /// <param name="data"></param>
      /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Mvc.HttpGet]
        public string GetContentById(decimal id)
        {
            var ret = new Response<string>();
            try
            {
                var service = Instance<ISystemService>.Create;
                var c = service.GetContentById(id);
                if (string.IsNullOrEmpty(c))
                {
                    return "抱歉，资讯无法显示!";
                }
                return c;

            }
            catch (Exception ex)
            {
                return "抱歉，资讯无法显示!";
            }
        }
        /// <summary>
        /// 获取最新的10条资讯
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<List<NewsDTO>> QueryTop10News()
        {
            var ret = new Response<List<NewsDTO>>();
            try
            {
                var service = Instance<ISystemService>.Create;
                var news = service.GetTop10News();
                var lst = new List<NewsDTO>();
                var host = (Request.RequestUri.Scheme+"://"+Request.RequestUri.Authority);
                news.ForEach(e => lst.Add(new NewsDTO() { id = e.id, title = e.title,img_url=string.IsNullOrEmpty(e.img_url)?"":host+e.img_url,create_date = e.create_date }));
                if (lst.Count > 0)
                {
                    ret.data=lst;
                }
                return ret;

            }
            catch (Exception ex)
            {
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }


        }

    }
}
