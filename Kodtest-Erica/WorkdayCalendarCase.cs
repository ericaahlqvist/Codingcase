namespace WorkdayCalendarCase.Domain
{
    public class WorkdayCalendar
    {
        private Workday _workday = default!;
        private WorkdayPolicy _workdayPolicy;
        private WorkdayService _workdayService;

        public WorkdayCalendar(WorkdayPolicy workdayPolicy, WorkdayService workdayService, Workday workday)
        {
            _workdayPolicy = workdayPolicy;
            _workdayService = workdayService;
            _workday = workday;
        }

        public DateTime CalculateWorkday(DateTime start, double numberOfWorkday)
        {
            // If the starttime is outside of the workday we need to align it to a workday
            // keeping in mind that the shift input decides if we should start calculating from next day or end of shift previous day
            // When aligning to workday we do not check if date is actually a workday
            // we only adjust to the work scheduele.

            int direction = numberOfWorkday >= 0 ? 1 : -1;
            double workdaysToShift = Math.Abs(numberOfWorkday);

            DateTime current = _workdayService.AlignToWorktime(start, _workday, direction);

            // We shift full days first
            while (workdaysToShift >= 1)
            {
                current = _workdayService.MoveToNextWorkday(current, direction);
                workdaysToShift -= 1;
            }

            // When all full days are shifted we handle the rest as minutes
            if (workdaysToShift > 0)
            {
                current = _workdayService.MoveMinutesWithinWorkday(current, _workday, workdaysToShift, direction);
            }

            // Result shouldnt include seconds.
            return new DateTime(
                    current.Year,
                    current.Month,
                    current.Day,
                    current.Hour,
                    current.Minute,
                    0
            );
        }
    }
}

