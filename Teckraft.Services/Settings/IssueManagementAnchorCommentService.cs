using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;

namespace Teckraft.Services.Settings
{
    public class IssueManagementAnchorCommentService : IService<IssueManagementAnchorComment>
    {
        private readonly IRepository<IssueManagementAnchorComment> issueRepository;
        private readonly IRepository<User> userRepository;

        public IssueManagementAnchorCommentService(IRepository<IssueManagementAnchorComment> issueRepository, IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
            this.issueRepository = issueRepository;
        }

        public List<IssueManagementAnchorComment> ToList
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IssueManagementAnchorComment Add(IssueManagementAnchorComment entity)
        {
            return this.issueRepository.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IssueManagementAnchorComment GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public ListQueryResult<IssueManagementAnchorComment> GetByQuery(ListQuery<IssueManagementAnchorComment> query)
        {
            return this.issueRepository.GetByQuery(query);
        }

        public IssueManagementAnchorComment Update(IssueManagementAnchorComment entity)
        {
            throw new NotImplementedException();
        }
    }
}
