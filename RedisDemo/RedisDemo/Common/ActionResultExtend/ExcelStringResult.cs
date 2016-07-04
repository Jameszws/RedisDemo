using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RedisDemo.Common.ActionResultExtend
{
    public class ExcelStringResult : ActionResult
    {

        public ExcelStringResult(string data, string fileName)
        {
            this.Data = data;
            this.FileName = fileName;
        }



        public string Data { get; set; }

        public string FileName { get; set; }



        public override void ExecuteResult(ControllerContext context)
        {
            if (string.IsNullOrEmpty(this.Data))
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }

            SetResponse(context);
        }



        /// <summary>

        /// 设置并向客户端发送请求响应。

        /// </summary>

        /// <param name="context"></param>

        private void SetResponse(ControllerContext context)
        {



            byte[] bytestr = Encoding.Default.GetBytes(this.Data);



            context.HttpContext.Response.Clear();

            context.HttpContext.Response.ClearContent();

            context.HttpContext.Response.Buffer = true;

            context.HttpContext.Response.Charset = "UTF-8";

            context.HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;

            context.HttpContext.Response.ContentType = "application/vnd.ms-excel";


            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(this.FileName + ".xls"));

            context.HttpContext.Response.AddHeader("Content-Length", bytestr.Length.ToString());

            context.HttpContext.Response.Write(this.Data);

            //context.HttpContext.Response.End();//不能加end操作

        }

    }
}
