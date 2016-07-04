using System;
using System.Web;
using System.Web.Mvc;

namespace RedisDemo.Common.ActionResultExtend
{
    /// <summary>
    /// 
    /// </summary>
    public class StringWriteResult : ActionResult
    {
        /// <summary>
        /// 要输入的数据
        /// </summary>
        private object Data { get; set; }

        private string ContentType { get; set; }

        /// <summary>
        /// Camel格式的json结果
        /// </summary>
        /// <param name="data">数据</param>
        public StringWriteResult(object data,string contentType)
        {
            this.Data = data;
            this.ContentType = contentType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = this.ContentType;
            response.Write(this.Data);
        }


    }
}
