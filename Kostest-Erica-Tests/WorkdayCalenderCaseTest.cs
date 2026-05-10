using WorkdayCalendarCase.Domain;

namespace WorkdayCalendarCase.Tests
{
    public class HolidayTest
    {
        [Fact]
        public void Should_Add_And_Check_Single_Holiday()
        {
            var holidayService = new HolidayService();

            var singleHoliday = new DateTime(2026, 5, 3);

            holidayService.AddSingleHoliday(singleHoliday);

            var checkSingleHoliday = holidayService.IsHoliday(singleHoliday);

            Assert.True(checkSingleHoliday);
        }
    }

    public class WorkdayTest
    {
        [Fact]
        public void Should_Return_True_Workday_On_Weekday()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);
            var workday = new DateTime(2026, 5, 6);

            var checkIfWorkday = workdayPolicy.IsWorkday(workday);
            Assert.True(checkIfWorkday);
        }

        [Fact]
        public void Should_Return_False_Workday_On_Weekend()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);
            var workday = new DateTime(2026, 5, 3);

            var checkIfWorkday = workdayPolicy.IsWorkday(workday);

            Assert.False(checkIfWorkday);
        }

        // ---------
        // Provided tests from instructions
        // ---------
        [Fact]
        public void Should_Return_Correct_Workday_1()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);
            var WorkdayService = new WorkdayService(workdayPolicy);

            var calendarCase = new WorkdayCalendar(workdayPolicy, WorkdayService, new Workday(new TimeSpan(08, 00, 00), new TimeSpan(16, 00, 00)));

            var workday = new DateTime(2004, 5, 24, 18, 05, 00);

            holidayService.AddSingleHoliday(new DateTime(2004, 5, 27));
            holidayService.AddRecurringHoliday(17, 5);

            var newDate = calendarCase.CalculateWorkday(workday, -5.5);

            Assert.Equal(new DateTime(2004, 5, 14, 12, 00, 00), newDate);
        }

        [Fact]
        public void Should_Return_Correct_Workday_2()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);
            var WorkdayService = new WorkdayService(workdayPolicy);

            var calendarCase = new WorkdayCalendar(workdayPolicy, WorkdayService, new Workday(new TimeSpan(08, 00, 00), new TimeSpan(16, 00, 00)));

            var workday = new DateTime(2004, 5, 24, 19, 03, 00);

            holidayService.AddSingleHoliday(new DateTime(2004, 5, 27));
            holidayService.AddRecurringHoliday(17, 5);

            var newDate = calendarCase.CalculateWorkday(workday, 44.723656);

            Assert.Equal(new DateTime(2004, 7, 27, 13, 47, 00), newDate);
        }

        // This test fails with the returned value beeing 2004-05-13T10:01:00.0000000
        [Fact]
        public void Should_Return_Correct_Workday_3()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);
            var WorkdayService = new WorkdayService(workdayPolicy);

            var calendarCase = new WorkdayCalendar(workdayPolicy, WorkdayService, new Workday(new TimeSpan(08, 00, 00), new TimeSpan(16, 00, 00)));

            var workday = new DateTime(2004, 5, 24, 18, 03, 00);

            holidayService.AddSingleHoliday(new DateTime(2004, 5, 27));
            holidayService.AddRecurringHoliday(17, 5);

            var newDate = calendarCase.CalculateWorkday(workday, -6.7470217);
            Assert.Equal(new DateTime(2004, 5, 13, 10, 02, 00), newDate);
        }

        [Fact]
        public void Should_Return_Correct_Workday_4()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);
            var WorkdayService = new WorkdayService(workdayPolicy);

            var calendarCase = new WorkdayCalendar(workdayPolicy, WorkdayService, new Workday(new TimeSpan(08, 00, 00), new TimeSpan(16, 00, 00)));

            var workday = new DateTime(2004, 5, 24, 08, 03, 00);

            holidayService.AddSingleHoliday(new DateTime(2004, 5, 27));
            holidayService.AddRecurringHoliday(17, 5);

            var newDate = calendarCase.CalculateWorkday(workday, 12.782709);
            Assert.Equal(new DateTime(2004, 6, 10, 14, 18, 00), newDate);
        }

        [Fact]
        public void Should_Return_Correct_Workday_5()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);
            var WorkdayService = new WorkdayService(workdayPolicy);

            var calendarCase = new WorkdayCalendar(workdayPolicy, WorkdayService, new Workday(new TimeSpan(08, 00, 00), new TimeSpan(16, 00, 00)));

            var workday = new DateTime(2004, 5, 24, 07, 03, 00);

            holidayService.AddSingleHoliday(new DateTime(2004, 5, 27));
            holidayService.AddRecurringHoliday(17, 5);

            var newDate = calendarCase.CalculateWorkday(workday, 8.276628);
            Assert.Equal(new DateTime(2004, 6, 4, 10, 12, 00), newDate);
        }
    }

    public class WorkdayAndHolidayTest
    {
        [Fact]
        public void Should_Return_False_Workday_On_Weekday_And_Reccuring_Holiday()
        {
            var holidayService = new HolidayService();
            var workdayPolicy = new WorkdayPolicy(holidayService);

            var workday = new DateTime(2026, 5, 6);

            holidayService.AddRecurringHoliday(6, 5);

            var checkIfWorkday = workdayPolicy.IsWorkday(workday);

            Assert.False(checkIfWorkday);
        }
    }
}

