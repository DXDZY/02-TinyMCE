using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class dalContentOperation
    {
        string connectionString = "Server=localhost;Database=sh_data;Uid=root;Pwd=xizhu211";
       
        //如果页面的menuid和userid库中对应存在则更新
        /// <summary>
        /// 根据页面传来的id获取对应内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable getContentModel(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM sh_data.content where id=?id";
                cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
        /// <summary>
        /// 插入新的页面内容
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="userID"></param>
        /// <param name="contentTitle"></param>
        /// <param name="contentText"></param>
        /// <param name="submitType"></param>
        /// <returns></returns>
        public bool insertContentModel(int menuID,int userID,string contentTitle,string contentText,int submitType)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `sh_data`.`content` (`menu_id`, `user_id`, `content_title`, `content_text`, `enabled`, `update_time`) 
                                    VALUES (?menuID, ?userID, ?contentTitle, ?contentText, ?submitType, ?updateTime);";
                cmd.Parameters.Add("?menuID", MySqlDbType.Int32).Value = menuID;
                cmd.Parameters.Add("?userID", MySqlDbType.Int32).Value = userID;
                cmd.Parameters.Add("?contentTitle", MySqlDbType.VarChar).Value = contentTitle;
                cmd.Parameters.Add("?contentText", MySqlDbType.VarChar).Value = contentText;
                cmd.Parameters.Add("?submitType", MySqlDbType.Int32).Value = submitType;
                cmd.Parameters.Add("?updateTime", MySqlDbType.Date).Value = DateTime.Now;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="userID"></param>
        /// <param name="contentTitle"></param>
        /// <param name="contentText"></param>
        /// <param name="submitType"></param>
        /// <returns></returns>
        public bool updateContentModel(string contentTitle, string contentText, int submitType,int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE `sh_data`.`content` SET 
                `content_title`=?contentTitle,
                `content_text`=?contentText, 
                `enabled`=?submitType, 
                `update_time`=?submitType 
                WHERE `id`=?id";
                //cmd.Parameters.Add("?menuID", MySqlDbType.Int32).Value = menuID;
                //cmd.Parameters.Add("?userID", MySqlDbType.Int32).Value = userID;
                cmd.Parameters.Add("?contentTitle", MySqlDbType.VarChar).Value = contentTitle;
                cmd.Parameters.Add("?contentText", MySqlDbType.VarChar).Value = contentText;
                cmd.Parameters.Add("?submitType", MySqlDbType.Int32).Value = submitType;
                cmd.Parameters.Add("?updateTime", MySqlDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

    }
}
