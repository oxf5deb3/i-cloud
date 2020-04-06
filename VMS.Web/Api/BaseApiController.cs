using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VMS.Controllers;
using VMS.DTO;
using VMS.Models;

namespace VMS.Api
{
    [WebApiExceptionFilterAttribute]
    [SwaggerIgnore]
    public class BaseApiController : ApiController
    {
        protected LoginDTO operInfo = GlobalVar.get();
    }
}
