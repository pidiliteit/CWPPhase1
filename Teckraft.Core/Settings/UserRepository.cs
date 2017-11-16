using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;

namespace Teckraft.Data.Settings
{
    public class UserRepository : IRepository<User>
    {
        private Mappings.IMappingProvider<User, Sql.UserDetail> _mapper;
        public UserRepository(Mappings.IMappingProvider<User, Sql.UserDetail> mapper)
        {
            this._mapper = mapper;
        }
        public List<User> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public ListQueryResult<User> GetByQuery(ListQuery<User> query)
        {
            var result = new ListQueryResult<User>();
            using (Sql.InitiativeHubFinalEntities dbcontext = new Sql.InitiativeHubFinalEntities())
            {
                var linqQuery = dbcontext.UserDetails.Where(it => it.UserId > 0);
                if (query.Parameters != null)
                {
                    foreach (var p in query.Parameters)
                    {
                        if (p.Name == "username")
                        {
                            if (p.Operator == CompareOperator.Equals)
                                linqQuery = linqQuery.Where(it => it.UserName == p.Value);
                        }
                        else if (p.Name == "title")
                        {
                            if (p.Operator == CompareOperator.Equals)
                                linqQuery = linqQuery.Where(it => it.Title == p.Value);
                        }
                        else if (p.Name.ToLower() == "dhdivisionid")
                        {
                            int val= 0;
                            val = int.Parse(p.Value);
                            linqQuery = linqQuery.Where(it => it.DepartmentHeads.Count(g=> g.DeparmentId==val) >0 );
                        }
                        else if (p.Name.ToLower() == "dodivisionid")
                        {
                            int val = 0;
                            val = int.Parse(p.Value);
                            linqQuery = linqQuery.Where(it => it.DepartmentOwners.Count(g => g.DeparmentId == val) > 0);
                        }
                        else if (p.Name == "ResponsibleUsers")
                        {
                            int val = 0;
                            val = int.Parse(p.Value);
                            linqQuery = linqQuery.Where(it => it.DivisionId == val);
                        }
                        else if (p.Name == "DependentUsers")
                        {
                            int val = 0;
                            val = int.Parse(p.Value);
                            linqQuery = linqQuery.Where(it => it.DivisionId == val);
                        }
                    }
                }

                foreach (var item in linqQuery)
                {
                    if (result.Items == null) result.Items = new List<User>();
                    var usr = _mapper.Map(item);
                 
                    result.Items.Add(usr);
                }
            }
            return result;
        }

        public User GetById(int id)
        {
            using (Sql.InitiativeHubFinalEntities dbcontext = new Sql.InitiativeHubFinalEntities())
            {
                return _mapper.Map(dbcontext.UserDetails.FirstOrDefault(it => it.UserId == id));
            }
            throw new Exception("Record not found!");
        }

        public User Add(User item)
        {
            throw new NotImplementedException();
        }

        public User Update(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
