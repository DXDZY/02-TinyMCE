using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Model;

namespace DAL
{
    public class GetData
    {
        string connectionString = "Server=localhost;Database=sh_data;Uid=root;Pwd=xizhu211";
        /// <summary>
        /// 获取菜单表
        /// </summary>
        /// <returns></returns>
        public DataTable getMenu(string power)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from menu where menu_power like ?power and enabled=1 order by  menu_order asc";
                cmd.Parameters.Add("?power", MySqlDbType.VarChar).Value ="%" + power + "%";
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
        /// 获取相同菜单行
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public DataTable getSameNameMenu(string menuName,int parentID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from menu where menu_cn_name = ?menuName and menu_parent_id=?parentID";
                cmd.Parameters.Add("?menuName", MySqlDbType.VarChar).Value = menuName;
                cmd.Parameters.Add("?parentID", MySqlDbType.Int32).Value = parentID;
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
        /// 生成新菜单id
        /// </summary>
        /// <returns></returns>
        public int GetNewMenuID()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select max(menu_id) from menu";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                DataTable dt = ds.Tables[0];
                int maxMenuID = Convert.ToInt32(dt.Rows[0][0]);
                return (maxMenuID+1);
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
        /// 插入新的一级菜单
        /// </summary>
        /// <param name="newMenuID"></param>
        public void insertNewFirstMenu(int newMenuID,MenuFirst mf)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `sh_data`.`menu` (`menu_id`, `menu_parent_id`, `menu_cn_name`, `menu_power`, `menu_url`) VALUES 
                                    (?newMenuID, '0', ?menuCnName, ?menuPower, ?menuUrl);";
                cmd.Parameters.Add("?newMenuID", MySqlDbType.Int32).Value = newMenuID;
                cmd.Parameters.Add("?menuCnName", MySqlDbType.VarChar).Value = mf.firstMenuName;
                cmd.Parameters.Add("?menuPower", MySqlDbType.VarChar).Value = mf.firstMenuPower;
                cmd.Parameters.Add("?menuUrl", MySqlDbType.VarChar).Value = mf.firstMenuNameUrl;
                cmd.ExecuteNonQuery();
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
        /// 插入新的二级菜单
        /// </summary>
        /// <param name="newMenuID"></param>
        public void insertNewSecondMenu(int newMenuID, MenuSecond ms , int parentID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `sh_data`.`menu` (`menu_id`, `menu_parent_id`, `menu_cn_name`, `menu_power`, `menu_url`) VALUES 
                                    (?newMenuID, ?parentID, ?menuCnName, ?menuPower, ?menuUrl);";
                cmd.Parameters.Add("?newMenuID", MySqlDbType.Int32).Value = newMenuID;
                cmd.Parameters.Add("?menuCnName", MySqlDbType.VarChar).Value = ms.secondMenuName;
                cmd.Parameters.Add("?menuPower", MySqlDbType.VarChar).Value = ms.secondMenuPower;
                cmd.Parameters.Add("?menuUrl", MySqlDbType.VarChar).Value = ms.secondMenuNameUrl;
                cmd.Parameters.Add("?parentID", MySqlDbType.Int32).Value = parentID;
                cmd.ExecuteNonQuery();
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
        /// 更新一级菜单
        /// </summary>
        /// <param name="newMenuID"></param>
        public void UpdateFirstMenu(MenuFirst mf)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE `sh_data`.`menu` SET `menu_cn_name`=?menuCnName, `menu_power`=?menuPower, `menu_url`=?menuUrl, `freeze`=?freeze  WHERE `menu_cn_name`=?menuCnName;";
                cmd.Parameters.Add("?menuCnName", MySqlDbType.VarChar).Value = mf.firstMenuName;
                cmd.Parameters.Add("?menuPower", MySqlDbType.VarChar).Value = mf.firstMenuPower;
                cmd.Parameters.Add("?menuUrl", MySqlDbType.VarChar).Value = mf.firstMenuNameUrl;
                cmd.Parameters.Add("?freeze", MySqlDbType.Int16).Value = mf.freezeMenu;
                cmd.ExecuteNonQuery();
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
        /// 更新二级菜单
        /// </summary>
        /// <param name="newMenuID"></param>
        public void UpdateSecondMenu(MenuSecond ms,int parentID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE `sh_data`.`menu` SET `menu_cn_name`=?menuCnName, `menu_power`=?menuPower, `menu_url`=?menuUrl, `freeze`=?freeze WHERE `menu_cn_name`=?menuCnName and menu_parent_id =?parentID;";
                cmd.Parameters.Add("?menuCnName", MySqlDbType.VarChar).Value = ms.secondMenuName;
                cmd.Parameters.Add("?menuPower", MySqlDbType.VarChar).Value = ms.secondMenuPower;
                cmd.Parameters.Add("?menuUrl", MySqlDbType.VarChar).Value = ms.secondMenuNameUrl;
                cmd.Parameters.Add("?parentID", MySqlDbType.Int32).Value = parentID;
                cmd.Parameters.Add("?freeze", MySqlDbType.Int16).Value = ms.freezeMenu;
                cmd.ExecuteNonQuery();
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
        /// 菜单排序
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="parentID"></param>
        public void UpdateMenuOrder(string[] menuNameArray, int parentID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                string sql = string.Empty;
                for (int i = 0, len = menuNameArray.Length; i < len; i++)
                {
                    sql += "UPDATE `sh_data`.`menu` SET `menu_order`=" + i + " WHERE `menu_cn_name`='" + menuNameArray[i] + "' and menu_parent_id =?parentID;";
                }
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                //cmd.CommandText = @"UPDATE `sh_data`.`menu` SET `menu_order`='0' WHERE `menu_cn_name`=?menuName and menu_parent_id =?parentID;";
                //cmd.Parameters.Add("?menuName", MySqlDbType.VarChar).Value = menuName;
                cmd.Parameters.Add("?parentID", MySqlDbType.Int32).Value = parentID;
                cmd.ExecuteNonQuery();
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
        /// 删除菜单
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="menuID"></param>
        /// <param name="parentID"></param>
        public void DeleteMenu(int menuID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE `sh_data`.`menu` SET `enabled`='0' WHERE `menu_parent_id`=?menuID;UPDATE `sh_data`.`menu` SET `enabled`='0' WHERE `menu_id`=?menuID;";
                //cmd.CommandText = @"UPDATE `sh_data`.`menu` SET `menu_order`='0' WHERE `menu_cn_name`=?menuName and menu_parent_id =?parentID;";
                //cmd.Parameters.Add("?menuName", MySqlDbType.VarChar).Value = menuName;
                cmd.Parameters.Add("?menuID", MySqlDbType.Int32).Value = menuID;
                cmd.ExecuteNonQuery();
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
        public void insert(string id,string name)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into a(id,name)values(@id,@name)";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@id", int.Parse(id));
                cmd.Parameters.AddWithValue("@name",name);
                cmd.ExecuteNonQuery();
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
    }
}
