using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
namespace TinyMCE
{
    /// <summary>
    /// handler 的摘要说明
    /// </summary>
    public class handler : IHttpHandler
    {
        bllContentOperation bco = new bllContentOperation();
        public void ProcessRequest(HttpContext context)
        {
            //string mytextarea = context.Request["mytextarea"].ToString();           
            int menuID=-2;
            if(context.Request["secondMenuLevelName"]!=null){
                menuID = Convert.ToInt32(context.Request["secondMenuLevelName"]);
            }
            if(context.Request["secondMenuName"]!=null){
                menuID = Convert.ToInt32(context.Request["secondMenuName"]);
            }
            if(menuID!=-2){
                string title = context.Request["title"].ToString();
                HttpPostedFile file = context.Request.Files["image"];   
                string formData = context.Request["formData"].ToString();
                formData = HttpUtility.UrlDecode(formData);
                int formIndex = formData.IndexOf('=');
                formData = formData.Substring(formIndex + 1);
                int submitType = Convert.ToInt32(context.Request["submitType"]);
                bool operationContent = bco.insertContentModel(menuID, 0, title, formData, submitType);
                context.Response.ContentType = "text/plain";
                context.Response.Write(operationContent);
            }
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