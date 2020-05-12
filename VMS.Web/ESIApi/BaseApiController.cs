using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VMS.ESIApi.Utils;

namespace VMS.ESIApi
{
    [ESIAuthCheck]
    [ESIAopFilter]
    [ESIExceptionTrace]
    public class BaseApiController : ApiController
    {
    }
}
