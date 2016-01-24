using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyMCE
{
    /// <summary>
    /// handler 的摘要说明
    /// </summary>
    public class handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //string mytextarea = context.Request["mytextarea"].ToString();

            HttpPostedFile file = context.Request.Files["image"];   
            string formData = context.Request["formData"].ToString();
            formData = HttpUtility.UrlDecode(formData);
            context.Response.ContentType = "text/plain";
            context.Response.Write(formData);
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}