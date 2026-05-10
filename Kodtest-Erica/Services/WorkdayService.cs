public class WorkdayService
{
    private readonly IWorkdayPolicy _workdayPolicy;
    public WorkdayService(IWorkdayPolicy workdayPolicy)
    {
        _workdayPolicy = workdayPolicy;
    }

    public DateTime AlignToWorktime(DateTime start, Workday workday, int direction)
    {
        var workdayStart = workday.Start;
        var workdayEnd = workday.End;

        var beforeWorkday = workdayStart > start.TimeOfDay;
        var afterWorkday = workdayEnd <= start.TimeOfDay;

        if (direction >= 0)
        {
            if (beforeWorkday) return start.Date + workdayStart;
            if (afterWorkday) return start.Date.AddDays(direction) + workdayStart;
        }
        else if (direction < 0)
        {
            if (beforeWorkday) return start.Date.AddDays(direction) + workdayEnd;
            if (afterWorkday) return start.Date + workdayEnd;
        }

        return start;
    }
    public DateTime MoveToNextWorkday(DateTime date, int direction)
    {
        do
        {
            date = date.AddDays(direction);
        }
        while (!_workdayPolicy.IsWorkday(date));

        // We have already converted the incoming date to the working hours, no need to check if the time is during the workday
        return date;
    }

    public DateTime MoveMinutesWithinWorkday(DateTime date, Workday workday, double remainingTime, int direction)
    {
        double remainingMinutesToShift = workday.TotalWorkdayMinutes * remainingTime;
        double availableMinutesWorkday = direction == 1
                    ? (workday.End - date.TimeOfDay).TotalMinutes
                    : (date.TimeOfDay - workday.Start).TotalMinutes;

        DateTime next = MoveToNextWorkday(date, direction);

        if (availableMinutesWorkday <= 0)
        {
            return MoveMinutesWithinWorkday(next, workday, remainingTime, direction);
        }

        if (remainingMinutesToShift <= availableMinutesWorkday)
        {
            return date.AddMinutes(remainingMinutesToShift * direction);
        }

        double rest = remainingMinutesToShift - availableMinutesWorkday;

        return MoveMinutesWithinWorkday(next, workday, rest, direction);
    }
}