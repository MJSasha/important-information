using ImpInfApi.Services;
using Xunit.Abstractions;

namespace UnitTests
{
    public class ScheduleParserTests
    {
        private readonly ITestOutputHelper output;
        private readonly ScheduleParser scheduleParser = new();

        public ScheduleParserTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void HaveDaysTest_PageWithDays()
        {
            var result = scheduleParser.HaveDays(TestsData.PageWithSchedule);

            Assert.True(result);
        }

        [Fact]
        public void HaveDaysTest_PageWithoutDays()
        {
            var result = scheduleParser.HaveDays(TestsData.PageWithoutSchedule);

            Assert.True(!result);
        }

        [Fact]
        public void GetDaysTest_PageWithDays()
        {
            var dates = scheduleParser.GetDaysDates(TestsData.PageWithSchedule);

            dates.ForEach(d => output.WriteLine(d.ToString()));

            Assert.Contains(DateTime.Parse("01.09.2023"), dates);
            Assert.Contains(DateTime.Parse("02.09.2023"), dates);
        }

        [Fact]
        public void GetDaysTest_PageWithoutDays()
        {
            var dates = scheduleParser.GetDaysDates(TestsData.PageWithoutSchedule);

            Assert.Empty(dates);
        }
    }
}