public class Workday
{
    public TimeSpan Start { get; }
    public TimeSpan End { get; }

    public Workday(TimeSpan start, TimeSpan end)
    {
        Start = start;
        End = end;
    }

    public double TotalWorkdayMinutes => (End - Start).TotalMinutes;
}