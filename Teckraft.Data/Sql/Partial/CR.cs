using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Teckraft.Data.Sql
{
    public partial class CR
    {
        public Reason RejectReason { get { return this.
            Reason; } }
        public Reason SendBackReason { get { return this.Reason2; } }
        public UserDetail PendingWithUserEntity { get { return this.UserDetail5; } }
        public UserDetail HODApproverUser { get { return this.UserDetail6; } }
        public Reason OnHOldReason { get { return this.Reason2; } }
        public UserDetail ProcessApprover { get { return this.UserDetail; } }
        public UserDetail RCBUser
        {
            get
            {
                return this.UserDetail1;
            }
        }
        public UserDetail RUBUser
        {
            get
            {
                return this.UserDetail2;
            }
        }

        public UserDetail ITEvaluatorUser
        {
            get
            {
                return this.UserDetail4;
            }
        }

        public UserDetail ITFHUser1
        {
            get
            {
                return this.UserDetail3;
            }
        }

    }
}
