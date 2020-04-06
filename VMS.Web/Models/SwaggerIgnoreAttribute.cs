using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS.Models
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SwaggerIgnoreAttribute : Attribute
    {
    }
}