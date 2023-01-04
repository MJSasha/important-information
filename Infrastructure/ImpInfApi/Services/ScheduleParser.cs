using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImpInfApi.Interfaces;
using ImpInfCommon.Data.Models;

namespace ImpInfApi.Services
{
    public class ScheduleParser : IScheduleParser
    {
        public Task<List<Day>> GetSchedule()
        {
            throw new System.NotImplementedException();
        }

        public bool HaveDays(string schedule)
        {
            return schedule.Contains("<span class=\"step-title ms-3 ms-sm-0 mt-2 mb-4 mb-sm-2 py-1 text-body\">");
        }

        public string GetDays(string schedule)
        {
            var regexDays = @"<span class=""step-title ms-3 ms-sm-0 mt-2 mb-4 mb-sm-2 py-1 text-body"">\w*(?<FirstWord>\w+)\w*</span>";
            var regex = new Regex(regexDays);
            var match = regex.Match(schedule);

            return match.Groups["FirstWord"].Value;
        }
    }
}
