using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
        DateTime TodayLocal { get; } 
    }
}
