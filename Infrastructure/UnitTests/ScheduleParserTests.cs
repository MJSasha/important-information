using ImpInfApi.Services;

namespace UnitTests
{
    public class ScheduleParserTests
    {
        private readonly ScheduleParser scheduleParser = new();

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
        public void GetDaysTest_PageWitDays()
        {
            var result = scheduleParser.GetDays(TestsData.PageWithSchedule);

            Assert.NotEmpty(result);
        }
    }
}