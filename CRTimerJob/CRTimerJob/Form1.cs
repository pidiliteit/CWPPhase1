using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Http;
using System.Net.Http.Headers;


namespace CRTimerJob
{

    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }


        public bool ForcefullLogout { get; set; }
    }


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        { 
             try
            {
                
                var query = "SendMail";
                var login = new LoginModel() { UserName = "itadmin", Password = "abc@1234", ForcefullLogout = true, RememberMe = true };

                HttpClient client = new HttpClient();
                //     Uri gizmoUri = null;

                var loginresult = client.PostAsJsonAsync(System.Configuration.ConfigurationSettings.AppSettings["ApplicationUrl"] + "/account/login", login).Result;
                var aa = loginresult.StatusCode;
                var result = client.PostAsJsonAsync(System.Configuration.ConfigurationSettings.AppSettings["ApplicationUrl"] + "/api/TimerJob", query).Result;

                var aaa = result.IsSuccessStatusCode;
            }
            catch (Exception ex) { }
            finally
            {
                //var service=
                Application.Exit();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var query = "Reminders";
                var login = new LoginModel() { UserName = "itadmin", Password = "abc@1234", ForcefullLogout = true, RememberMe = true };

                HttpClient client = new HttpClient();
                //     Uri gizmoUri = null;

                var loginresult = client.PostAsJsonAsync(System.Configuration.ConfigurationSettings.AppSettings["ApplicationUrl"] + "/account/login", login).Result;
                var aa = loginresult.StatusCode;
                var result = client.PostAsJsonAsync(System.Configuration.ConfigurationSettings.AppSettings["ApplicationUrl"] + "/api/TimerJob", query).Result;

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
