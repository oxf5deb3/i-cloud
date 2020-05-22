using Swashbuckle.Swagger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace VMS.App_Start
{
    public class SwaggerControllerDescProvider : ISwaggerProvider
    {
        private readonly ISwaggerProvider _swaggerProvider;
        private static ConcurrentDictionary<string, SwaggerDocument> _cache = new ConcurrentDictionary<string, SwaggerDocument>();
        private readonly string _xml;
        public SwaggerControllerDescProvider(ISwaggerProvider swaggerProvider, string xml)
        {
            _swaggerProvider = swaggerProvider;
            _xml = xml;
        }
        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            //只读取一次
            if (!_cache.TryGetValue(cacheKey, out srcDoc))
            {
                srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);
                //srcDoc.paths.
                srcDoc.vendorExtensions = new Dictionary<string,object>() { { "ControllerDesc", GetControllerDesc() } };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }
        /// 

        /// 从API文档中读取控制器描述
        /// 
        /// 所有控制器描述
        public ConcurrentDictionary<string,string> GetControllerDesc()
        {
            string xmlpath = _xml;
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            if (File.Exists(xmlpath))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlpath);
                string type = string.Empty, path = string.Empty, controllerName = string.Empty;

                string[] arrPath;
                int length = -1, cCount = "Controller".Length;
                XmlNode summaryNode = null;
                foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                {
                    var allparam = node.SelectNodes("param");
                    if (allparam.Count == 0) continue;
                    var lst = new List<string>();
                    foreach(XmlNode n in allparam)
                    {
                        var name = n.Attributes["name"].Value;
                        var t = n.InnerText.Trim();
                        var str = name + ":" + t;
                        lst.Add(str);
                    }
                    type = node.Attributes["name"].Value;
                    if (type.Contains("Controller"))
                    {
                        //控制器
                        var k = type.LastIndexOf("(");
                        if (k != -1)
                        {
                            type = type.Substring(0, k);
                        }
                        arrPath = type.Split('.');
                        length = arrPath.Length;
                        controllerName = arrPath[length - 2];
                        
                        if (controllerName.EndsWith("Controller"))
                        {
                            var action = arrPath[length -1];
                            //获取控制器注释
                            summaryNode = node.SelectSingleNode("summary");
                            string key = controllerName.Remove(controllerName.Length - cCount, cCount)+"_"+ action;
                            if (lst.Any() && !controllerDescDict.ContainsKey(key))
                            {
                                controllerDescDict.TryAdd(key, string.Join("@",lst));
                            }
                        }
                    }
                }
            }
            return controllerDescDict;
        }
    }
}