using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TinyMCE
{
    /// <summary>
    /// uploadImage 的摘要说明
    /// </summary>
    public class uploadImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["image"];
            //string savePath = @"C:/下载/";
            //file.SaveAs(savePath + file.FileName);
            //string url = savePath+file.FileName;
            //string jsonpath = "{\"location \":\"/images/" + file.FileName + "\"}";
            string uploadPath =
            HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\images\\";
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                file.SaveAs(uploadPath + file.FileName);
                context.Response.Write("images/" + file.FileName);
            }
            else
            {
                context.Response.Write("0");
            }  
            context.Response.ContentType = "text/plain";
            //context.Response.Write(url);
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