using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMS.ESIApi.Utils;

namespace VMS.ESIApi.Models
{
    public class Response<T> where T:class
    {
        public Response()
        {
            success = true;
            data =default(T);
            code = StatusCode.SUCCESS;
            message = "";
        }
        public StatusCode code { get; set; }

        public bool success { get; set; }

        public string message { get; set; }

        public T data { get; set; }
    }

    public class DefaultResponse
    {
        public static Response<string> SuccessWithoutData()
        {
            var resp = new Response<string>();
            resp.code = StatusCode.SUCCESS;
            resp.message = StatusCode.SUCCESS.FetchDescription();
            return resp;
        }

        public static Response<T> SuccessWithData<T>(T data) where T : class, new()
        {
            var resp = new Response<T>();
            resp.code = StatusCode.SUCCESS;
            resp.message = StatusCode.SUCCESS.FetchDescription();
            resp.data = data;
            return resp;
        }

        public static Response<T> Fail<T>(StatusCode code) where T:class
        {
            var resp = new Response<T>();
            resp.code = code;
            resp.message = code.FetchDescription();
            return resp;
        }


    }
}