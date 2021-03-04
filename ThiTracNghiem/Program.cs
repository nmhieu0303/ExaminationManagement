using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiTracNghiem
{
    static class Program
    {
        public static string DBName = "QLTTN";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string conStr = myDbHelper.GetConnectionStringFromAppConfig("ThiTracNghiem.Properties.Settings.QLTTNConnectionString");
            if (myDbHelper.CheckQLTTN(conStr))
            {
                Application.Run(new frmLogin());
            }
            else
            {
                // nếu không kết nối được thì sẽ phải vao formAdmin để cấu hình
                Application.Run(new frmAdmin());
            }
        }
    }
}