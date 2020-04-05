using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VMS.ESIApi
{
    public class QRCodeController : BaseApiController
    {
        [HttpGet]
        public int add(int a,int b)
        {
            return a + b;
        }
    }
}
