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
                string fileName = string.Empty;
                int fileSplit = file.FileName.LastIndexOf('\\');
                if (fileSplit > 0)
                {
                    fileName = file.FileName.Substring(fileSplit + 1);
                }
                else
                {
                    fileName = file.FileName;
                }
                file.SaveAs(uploadPath + fileName);
                context.Response.Write("images/" + fileName);
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