using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace VMS.Models
{
    public class SwaggerIgnoreFilter:IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            foreach (ApiDescription apiDescription in apiExplorer.ApiDescriptions)
            {
                var ignore = apiDescription.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<SwaggerIgnoreAttribute>().ToList();
                var _key = "/" + apiDescription.RelativePath.TrimEnd('/');
                // 过滤 swagger 自带的接口
                if (_key.Contains("/api/Swagger") && swaggerDoc.paths.ContainsKey(_key))
                    swaggerDoc.paths.Remove(_key);
                var fullName = apiDescription.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;
                var controllerName = apiDescription.ActionDescriptor.ControllerDescriptor.ControllerName;
                var actionName = apiDescription.ActionDescriptor.ActionName;
                if (ignore.Count>0 || controllerName.ToUpper().EndsWith("API") || !fullName.Contains("VMS.ESIApi") || !_key.Contains("esi-api") || apiDescription.HttpMethod.Method.ToUpper()=="GET")
                {
                    var qmark = _key.IndexOf("?");
                    if (qmark >= 0)
                    {
                        _key = _key.Substring(0, qmark);
                    }
                    swaggerDoc.paths.Remove(_key);
                }
                //var _key = "/" + apiDescription.RelativePath.TrimEnd('/');
                //// 过滤 swagger 自带的接口
                //if (!_key.Contains("/esi-api/") && swaggerDoc.paths.ContainsKey(_key))
                //    swaggerDoc.paths.Remove(_key);
            }
        }
    }
}