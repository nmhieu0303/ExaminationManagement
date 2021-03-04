using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiTracNghiem
{
    public class myDbHelper
    {
        /// <summary>
        /// lấy danh sách các databasename có trong server này
        /// </summary>
        /// <param name="servername"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static List<string> GetListDatabaseName(string servername, string username, string password)
        {
            string connectionString =
                  @"Server=" + servername + ";"
                  + "Database=master;"
                  + "User Id=" + username + ";Password=" + password + ";";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    List<string> lstr = new List<string>();
                    using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lstr.Add(dr[0].ToString());
                            }
                        }
                    }
                    con.Close();
                    return lstr;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// lấy danh sách tên các database có trong server này
        /// </summary>
        /// <param name="servername"></param>
        /// <returns></returns>
        public static List<string> GetListDatabaseName(string servername)
        {
            string connectionString = @"Data Source=" + servername + ";Initial Catalog=master;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    List<string> lstr = new List<string>();
                    using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lstr.Add(dr[0].ToString());
                            }
                        }
                    }
                    con.Close();
                    return lstr;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }


        /// <summary>
        /// lấy về list các server, instante name. ví dụ: DESKTOP-ewk\KoiCHEN
        /// </summary>
        /// <returns></returns>
        public static List<string> GetListServerNameWithInstanceName()
        {
            // Retrieve the enumerator instance and then the data.  
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            System.Data.DataTable table = instance.GetDataSources();
            List<string> lstr = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                lstr.Add(row["ServerName"].ToString() + @"\" + row["InstanceName"].ToString());
            }
            return lstr;
        }

        /// <summary>
        /// ghi giá trị(value) của connectionString[name] vào file App.config
        /// </summary>
        /// <param name="name">name của tag connectionString trong file App.config</param>
        /// <param name="value">chuỗi giá trị của connection tring</param>
        public static void SaveConnectionString(string name, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings[name].ConnectionString = value;
            config.ConnectionStrings.ConnectionStrings[name].ProviderName = "System.Data.SqlClient";
            config.Save(ConfigurationSaveMode.Full, true);
        }

        public static string GetConnectionStringFromAppConfig(string name)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return config.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        }
        
        public static bool CheckConnection(string connectionString)
        {
            try
            {
                var con = new SqlConnection(connectionString);
                con.Open();
                con.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra xem connectiontring truyền vào có db name đúng với yêu cầu là "QLTTN" hay không
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool CheckQLTTN(string connectionString)
        {
            try
            {
                if (myDbHelper.CheckConnection(connectionString) &&
                    myDbHelper.GetDBName(connectionString) == Program.DBName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetDBName(string connectionString)
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connectionString);
            return sb.InitialCatalog;
        }
    }
}
