using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BLL;
using Model;

namespace shuhuawang.handler
{
    /// <summary>
    /// GetDataHandler 的摘要说明
    /// </summary>
    public class GetDataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                GetData gd = new GetData();
                string cmd = context.Request["cmd"].ToString();
                //创建菜单
                if (cmd == "getMenu")
                {
                    string userPower = context.Request["userPower"].ToString();
                    string power = string.Empty;
                    switch (userPower)
                    {
                        case "0":
                            power = "游客";
                            break;
                        case "1":
                            power = "管理员";
                            break;
                        default:
                            power = "游客";
                            break;
                    }
                    string menu = gd.getMenuJSON(power);
                    context.Response.Write(menu);
                }
                //检查菜单是否已经存在
                else if (cmd == "checkMenuName")
                {
                    context.Response.Write("false");
                }
                //存储一级菜单
                else if (cmd == "firstMenuSave")
                {
                    string form = HttpUtility.UrlDecode(context.Request["form"].ToString());
                    gd.insertNewFirstMenu(form);
                    context.Response.Write("1");
                }
                else if (cmd == "secondMenuSave")
                {
                    string form = HttpUtility.UrlDecode(context.Request["form"].ToString());
                    gd.insertNewSecondMenu(form);
                    context.Response.Write("1");
                }
                else if (cmd == "menuOrder")
                {
                    string menuList = HttpUtility.UrlDecode(context.Request["menuNameList"].ToString());
                    int parentID = Convert.ToInt32(context.Request["parentID"]);
                    gd.UpdateMenuOrder(menuList, parentID);
                    context.Response.Write("1");
                }
                else if (cmd == "menuDelete")
                {
                    //string menuName = HttpUtility.UrlDecode(context.Request["menuName"].ToString());
                    //int parentID = Convert.ToInt32(context.Request["parentID"]);
                    int menuID = Convert.ToInt32(context.Request["menuID"]);
                    gd.DeleteMenu(menuID);
                    context.Response.Write("1");
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("");
            }
            catch (Exception ex)
            {
                context.Response.Write("0");
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