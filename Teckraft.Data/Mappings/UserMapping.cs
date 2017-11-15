using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Sql;

namespace Teckraft.Data.Mappings
{
    

    public class UserMappingProvider : IMappingProvider<User, UserProfile>
    {
        public User Map(UserProfile item)
            
        {
            if (item == null) return null;
            return new User() { Id=item.UserId, UserName=item.UserName };
        }

        public UserProfile Map(User dbItem)
        {
            throw new NotImplementedException();
        }

 
    }
}

