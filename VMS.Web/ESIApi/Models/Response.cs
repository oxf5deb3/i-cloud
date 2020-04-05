using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMS.ESIApi.Utils;

namespace VMS.ESIApi.Models
{
    public class Response<T>
    {
        public StatusCode code { get; set; }

        public string message { get; set; }

        public T data { get; set; }
    }

    public class DefaultResponse<T>
    {
        public static Response<T> SuccessWithoutData()
        {
            var resp = new Response<T>();
            resp.code = StatusCode.SUCCESS;
            resp.message = StatusCode.SUCCESS.FetchDescription();
            return resp;
        }

        public static Response<T> SuccessWithData(T data)
        {
            var resp = new Response<T>();
            resp.code = StatusCode.SUCCESS;
            resp.message = StatusCode.SUCCESS.FetchDescription();
            resp.data = data;
            return resp;
        }

        public static Response<T> Fail(StatusCode code)
        {
            var resp = new Response<T>();
            resp.code = code;
            resp.message = code.FetchDescription();
            return resp;
        }


    }
}