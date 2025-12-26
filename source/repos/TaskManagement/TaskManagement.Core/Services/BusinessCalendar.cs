using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Core.Services
{
    public sealed class BusinessCalendar : IBusinessCalendar
    {
        private readonly HashSet<DateTime> _holidays;

        public BusinessCalendar(IConfiguration configuration)
        {
            var list = configuration.GetSection("BusinessCalendar:Holidays").Get<string[]>() ?? Array.Empty<string>();
            _holidays = new HashSet<DateTime>(
                list.Select(s => DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date)
            );
        }

        public bool IsWeekend(DateTime date)
            => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        public bool IsHoliday(DateTime date)
            => _holidays.Contains(date.Date);

        public bool IsBusinessDay(DateTime date)
            => !IsWeekend(date) && !IsHoliday(date);
    }
}
