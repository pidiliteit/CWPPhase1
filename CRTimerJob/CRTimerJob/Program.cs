using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CRTimerJob
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //System.Diagnostics.Process.Start("http://www.google.com");
            RunProcess();
        }

        public static void RunProcess()
        {

             try
            {
                var password = System.Configuration.ConfigurationManager.AppSettings["password"];
                var query = "Reminders";
                var login = new LoginModel() { UserName = "20162", Password = password, ForcefullLogout = true, RememberMe = true };

                
                HttpClient client = new HttpClient();
                //     Uri gizmoUri = null;

                var loginresult = client.PostAsJsonAsync(System.Configuration.ConfigurationManager.AppSettings["ApplicationUrl"] + "/account/login", login).Result;
                var aa = loginresult.StatusCode;
                var result = client.PostAsJsonAsync(System.Configuration.ConfigurationManager.AppSettings["ApplicationUrl"] + "/api/TimerJob", query).Result;
               // var result = client.GetAsync(System.Configuration.ConfigurationSettings.AppSettings["ApplicationUrl"] + "/api/TimerJob").Result;
                // System.Net.WebClient myclient= new System.Net.WebClient();

                 //var result = myclient.DownloadString("http://localhost:53113/api/TimerJob");

                var aaa = result.IsSuccessStatusCode;
            }
            catch (Exception ex) { }
            finally
            {
                //var service=
                Application.Exit();
            }
        }
    }
}
