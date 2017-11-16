using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;

namespace Teckraft.Services.Settings
{
    public class IssueManagementCommentLogService : IService<IssueManagementCommentLog>
    {
        private readonly IRepository<IssueManagementCommentLog> issueRepository;
        private readonly IRepository<User> userRepository;

        public IssueManagementCommentLogService(IRepository<IssueManagementCommentLog> issueRepository, IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
            this.issueRepository = issueRepository;
        }

        public List<IssueManagementCommentLog> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public Data.ListQueryResult<IssueManagementCommentLog> GetByQuery(Data.ListQuery<IssueManagementCommentLog> query)
        {
            return this.issueRepository.GetByQuery(query);
        }

        public IssueManagementCommentLog GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public IssueManagementCommentLog Add(IssueManagementCommentLog entity)
        {
            return this.issueRepository.Add(entity);
        }

        public IssueManagementCommentLog Update(IssueManagementCommentLog entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
