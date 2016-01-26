using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using DAL;
using Model;
namespace BLL
{
    public class GetData
    {
        DAL.GetData gd = new DAL.GetData();
        /// <summary>
        /// 获取菜单json字符串
        /// </summary>
        /// <param name="userPower"></param>
        /// <returns></returns>
        public string getMenuJSON(string userPower)
        {
            string menu = string.Empty;
            DataTable dt = gd.getMenu(userPower);
            menu += "[";
            foreach (DataRow dr in dt.Rows)
            {
                DataRow[] drCheck = dt.Select("menu_id=" + dr["menu_parent_id"]);
                if (drCheck.Length == 0)
                {
                    menu += "{";
                    menu += "\"menu_id\":\"" + dr["menu_id"] + "\",";
                    menu += "\"menu_parent_id\":\"" + dr["menu_parent_id"] + "\",";
                    menu += "\"menu_cn_name\":\"" + dr["menu_cn_name"] + "\",";
                    menu += "\"menu_url\":\"" + dr["menu_url"] + "\",";
                    menu += "\"menu_power\":\"" + dr["menu_power"] + "\",";
                    menu += "\"freeze\":\"" + dr["freeze"] + "\",";
                    DataRow[] drs = dt.Select("menu_parent_id =" + dr["menu_id"]);
                    if (drs.Length > 0)
                    {
                        menu += "\"child\":[";
                        foreach (DataRow drChild in drs)
                        {
                            menu += "{";
                            menu += "\"menu_id\":\"" + drChild["menu_id"] + "\",";
                            menu += "\"menu_parent_id\":\"" + drChild["menu_parent_id"] + "\",";
                            menu += "\"menu_cn_name\":\"" + drChild["menu_cn_name"] + "\",";
                            menu += "\"menu_url\":\"" + drChild["menu_url"] + "\",";
                            menu += "\"menu_power\":\"" + drChild["menu_power"] + "\",";
                            menu += "\"freeze\":\"" + drChild["freeze"] + "\",";
                            menu += "\"child\":\"\"";
                            menu += "},";
                        }
                        menu = menu.Substring(0, menu.Length - 1);
                        menu += "]";
                    }
                    else
                    {
                        menu += "\"child\":\"\"";
                    }
                    menu += "},";
                }
            }
            menu = menu.Substring(0,menu.Length-1);
            menu += "]";
            return menu;
        }
        /// <summary>
        /// 生成新菜单id
        /// </summary>
        /// <returns></returns>
        public int GetNewMenuID()
        {
            int newMenuID = gd.GetNewMenuID();
            return newMenuID;
        }
        /// <summary>
        /// 插入新的一级菜单
        /// </summary>
        /// <param name="newMenuID"></param>
        /// <param name="mf"></param>
        public void insertNewFirstMenu(string form)
        {
            Dictionary<string, string> formDictionary = JsonTools.jsonToDictionary(form);
            MenuFirst menuFirst = new MenuFirst();
            menuFirst = JsonTools.PopulateEntityFromCollection(menuFirst, formDictionary);
            DataTable dtSameMenuName = gd.getSameNameMenu(menuFirst.firstMenuName,0);
            if (dtSameMenuName.Rows.Count > 0)
            {
                gd.UpdateFirstMenu(menuFirst);
            }
            else
            {
                int newID = gd.GetNewMenuID();
                gd.insertNewFirstMenu(newID, menuFirst);
            }
        }
        public void insertNewSecondMenu(string form)
        {
            Dictionary<string, string> formDictionary = JsonTools.jsonToDictionary(form);
            MenuSecond menuSecond = new MenuSecond();
            menuSecond = JsonTools.PopulateEntityFromCollection(menuSecond, formDictionary);
            //取得一级菜单
            DataTable dtParentMenu = gd.getSameNameMenu(menuSecond.secondMenuLevelName, 0);
            int parentID = 0;
            if (dtParentMenu.Rows.Count > 0)
            {
                parentID = Convert.ToInt32(dtParentMenu.Rows[0]["menu_id"]);
            }
            //取得二级菜单
            DataTable dtSameMenuName = gd.getSameNameMenu(menuSecond.secondMenuName,parentID);
            if (dtSameMenuName.Rows.Count > 0)
            {
                gd.UpdateSecondMenu(menuSecond, parentID);
            }
            else
            {
                int newID = gd.GetNewMenuID();
                gd.insertNewSecondMenu(newID, menuSecond, parentID);
            }

        }
        /// <summary>
        /// 获取相同菜单行
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public DataTable getSameNameMenu(string menuName,int parentID)
        {
            return gd.getSameNameMenu(menuName, parentID);
        }
        /// <summary>
        /// 菜单排序
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="parentID"></param>
        public void UpdateMenuOrder(string menuNameList,int parentID)
        {
            string[] menuNameArray = menuNameList.Split('&');

            gd.UpdateMenuOrder(menuNameArray, parentID);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="menuID"></param>
        /// <param name="parentID"></param>
        public void DeleteMenu(int menuID)
        {
            gd.DeleteMenu(menuID);
        }
    }
}
