using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace RedisDemo.Common.ActionResultExtend
{
    /// <summary>
    /// Camel格式的json结果
    /// </summary>
    public class JsonCamelResult : ActionResult
    {
        public object Data { get; set; }
        public JsonRequestBehavior JsonRequestBehavior { get; set; }
        public bool IsConver = true;

        /// <summary>
        /// Camel格式的json结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="jsonRequestBehavior">知否支持Get操作</param>
        public JsonCamelResult(object data, bool isConver = true, JsonRequestBehavior jsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.DenyGet)
        {
            this.Data = data;
            JsonRequestBehavior = jsonRequestBehavior;
            IsConver = isConver;
        }

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

            response.ContentType = "text/plain";
            if (this.Data != null)
            {
                if (IsConver)
                {
                    response.Write(JsonConvert.SerializeObject(this.Data));
                }
                else
                {
                    response.Write(this.Data);
                }
            }
        }
    }
}
