using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RedisDemo.Common.ActionResultExtend
{
    public class JsonpResult : JsonResult
    {
        public object Data { get; set; }
        public JsonRequestBehavior JsonRequestBehavior { get; set; }
        /// <summary>
        /// 时间格式化字符串
        /// </summary>
        public string DateTimeFormat { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("本地址不允许执行Get操作");
            }

            HttpResponseBase response = context.HttpContext.Response;

            string clientFunc = context.RequestContext.HttpContext.Request["jsonpcallback"].Trim();

            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = DateTimeFormat };
                string result = JsonConvert.SerializeObject(Data, dtConverter);
                response.Write(string.Format("{0}({1})", clientFunc, result));
            }
        }
    }
}
