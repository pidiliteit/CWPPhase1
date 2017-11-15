using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web;


namespace Teckraft.Web.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

  //  [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
       // [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public static UserProfile Current
        {
            get
            {
                if (System.Web.HttpContext.Current.Cache["User_Context_" + HttpContext.Current.User.Identity.Name] == null)
                {
                    
                    //BEContext context = new BEContext();
                    //UserProfile user = context.GetUser(HttpContext.Current.User.Identity.Name);
                    //System.Web.HttpContext.Current.Cache.Add("User_Context_" + HttpContext.Current.User.Identity.Name, user, null, DateTime.Now.AddMinutes(3), TimeSpan.Zero, CacheItemPriority.Default, null);

                }
                return System.Web.HttpContext.Current.Cache["User_Context_" + HttpContext.Current.User.Identity.Name] as UserProfile;
            }
        }

        //internal static UserProfile CreateFromDE(SingleSignOn.Models.DE.UserDetail userDetail)
        //{
        //    var up=new UserProfile() { UserId = userDetail.UserId, UserName = userDetail.UserName, FirstName = userDetail.FirstName, LastName = userDetail.LastName, Email = userDetail.Email, Mobile = userDetail.Mobile, Division = userDetail.Division, UserType = userDetail.UserType };
        //    if (userDetail.UserProfile.UserAlias != null && userDetail.UserProfile.UserAlias.Count > 0)
        //    {
        //        up.Alias = new List<Alias>();
        //        foreach (var item in userDetail.UserProfile.UserAlias)
        //        {
        //            up.Alias.Add(new Alias() {  AliasName=item.Alias, ApplicationCode=item.Application.ApplicationCode});
        //        }
        //    }
        //    return up;
        //}

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Division { get; set; }

        public string UserType { get; set; }
        //public List<Alias> Alias { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


         [Display(Name = "Forcefull Logout?")]
        public bool ForcefullLogout { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
