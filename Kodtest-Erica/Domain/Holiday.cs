public abstract class Holiday
{
    public abstract bool IsMatch(DateTime date);
}

public class SingleHoliday : Holiday
{
    private DateTime _date;

    public SingleHoliday(DateTime date)
    {
        _date = date.Date;
    }

    public override bool IsMatch(DateTime date)
    {
        // Return true if the provided date is a registered holiday
        return date.Date == _date;
    }
}

/** Recurring holidays should not include year **/
public class RecurringHoliday : Holiday
{
    private int _day;
    private int _month;

    public RecurringHoliday(int day, int month)
    {
        _day = day;
        _month = month;
    }

    public override bool IsMatch(DateTime date)
    {
        return date.Day == _day && date.Month == _month;
    }
}