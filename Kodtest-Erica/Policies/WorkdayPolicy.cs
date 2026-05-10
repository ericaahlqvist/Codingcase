
public interface IWorkdayPolicy
{
    bool IsWorkday(DateTime date);
}
public class WorkdayPolicy : IWorkdayPolicy
{
    private readonly HolidayService _holidayService;
    public WorkdayPolicy(HolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    public bool IsWeekend(DateTime date) =>
        date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;

    public bool IsWorkday(DateTime date) =>
        !IsWeekend(date) && !_holidayService.IsHoliday(date);
}