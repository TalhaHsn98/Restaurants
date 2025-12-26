using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Contracts
{
    public interface IBusinessCalendar
    {
        bool IsWeekend(DateTime date);
        bool IsHoliday(DateTime date);
        bool IsBusinessDay(DateTime date);
    }
}
