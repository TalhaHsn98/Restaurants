using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Core.Services
{
    public sealed class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime TodayLocal => DateTime.Now.Date;
    }
}
