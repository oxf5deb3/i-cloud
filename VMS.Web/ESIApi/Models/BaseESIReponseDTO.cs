using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMS.DTO;

namespace VMS.ESIApi.Models
{
    public class BaseESIReponseDTO: BaseResponseDTO
    {
        public BaseESIReponseDTO() : base()
        {
            code = StatusCode.SUCCESS;
        }

        public StatusCode code { get; set; }
    }
}