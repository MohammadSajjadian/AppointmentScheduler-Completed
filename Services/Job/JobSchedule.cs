using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Job
{
    public class JobSchedule
    {
        public Type jobType { get; set; }

        public string cronExpression { get; set; }

        public JobSchedule(Type _jobType, string _cronExpression)
        {
            jobType = _jobType;
            cronExpression = _cronExpression;
        }
    }
}
