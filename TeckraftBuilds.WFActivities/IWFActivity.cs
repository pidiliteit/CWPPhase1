using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeckraftBuilds.WFActivities
{
    interface IWFActivity
    {
        void Execute(WFContext context);
    }
}
