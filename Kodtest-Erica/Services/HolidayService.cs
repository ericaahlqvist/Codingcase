public class HolidayService
{
    private readonly List<Holiday> holidays = new List<Holiday>();

    public void AddSingleHoliday(DateTime date) =>
        holidays.Add(new SingleHoliday(date));

    public void AddRecurringHoliday(int day, int month) =>
        holidays.Add(new RecurringHoliday(day, month));

    public bool IsHoliday(DateTime date) =>
        holidays.Any(h => h.IsMatch(date));
}