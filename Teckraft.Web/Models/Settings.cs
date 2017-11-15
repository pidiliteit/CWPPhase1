
namespace Teckraft.Web.Models
{
    public class Settings
    {

        public static bool EnableOTP
        {
            get
            {
                bool enableotp = false;
                bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["EnableOTP"], out enableotp);
                return enableotp;
            }
        }

    }
}