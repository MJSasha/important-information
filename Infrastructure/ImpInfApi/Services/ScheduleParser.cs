using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImpInfApi.Interfaces;
using ImpInfCommon.Data.Models;

namespace ImpInfApi.Services
{
    public class ScheduleParser : IScheduleParser
    {
        #region RegexPatterns
        private readonly Regex dayDateRegex = new(@"<span class=""step-title ms-3 ms-sm-0 mt-2 mb-4 mb-sm-2 py-1 text-body"">\W*\w{2},&nbsp;(?<Date>\d*)&nbsp;(?<Month>\w*)");
        #endregion


        public Task<List<Day>> GetSchedule()
        {
            throw new System.NotImplementedException();
        }

        public bool HaveDays(string schedule)
        {
            return schedule.Contains("<span class=\"step-title ms-3 ms-sm-0 mt-2 mb-4 mb-sm-2 py-1 text-body\">");
        }

        public List<DateTime> GetDaysDates(string schedule)
        {
            var match = dayDateRegex.Matches(schedule);

            List<(string date, string month)> datesAndMonths = match.Select(m => (m.Groups["Date"].Value, MonthNameToNumber(m.Groups["Month"].Value))).ToList();

            return datesAndMonths
                .Select(dm => DateTime.Parse($"{dm.date}.{dm.month}.{(DateTime.Now.Month >= Convert.ToInt32(dm.month) ? DateTime.Now.Year + 1 : DateTime.Now.Year)}")).ToList();
        }

        private string MonthNameToNumber(string month)
        {
            return month.ToLower() switch
            {
                "января" => "01",
                "февраля" => "02",
                "марта" => "03",
                "апреля" => "04",
                "мая" => "05",
                "июня" => "06",
                "июля" => "07",
                "августа" => "08",
                "сентября" => "09",
                "октября" => "10",
                "ноября" => "11",
                "декабря" => "12",
                _ => "01",
            };
        }
    }
}
