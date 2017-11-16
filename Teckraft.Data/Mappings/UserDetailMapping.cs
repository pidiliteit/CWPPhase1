using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.SAP;
//using Teckraft.Core.Domian.SAP;
using Teckraft.Core.Domian.Settings;


namespace Teckraft.Data.Mappings
{
    public class UserDetailMapping : IMappingProvider<User, Sql.UserDetail>
    {
        private IMappingProvider<Division, Sql.Division> divisionMapper;
        public UserDetailMapping(IMappingProvider<Division, Sql.Division> divisionMapper)
        {
            this.divisionMapper = divisionMapper;
        }


        public User Map(Sql.UserDetail item)
        {
            if (item == null) return null;
            return new User() { Id = item.UserId, UserName = item.UserName, Email = item.Email, Title = item.Title, Division = divisionMapper.Map(item.Division),MobileNo=item.MobileNo};
        }

        public Sql.UserDetail Map(User dbItem)
        {
            return new Sql.UserDetail()
            {
                UserId = dbItem.Id,
                UserName = dbItem.UserName,
                Title = dbItem.Title,

            };
        }

        public bool IsTeamMember { get; set; }
    }
}
