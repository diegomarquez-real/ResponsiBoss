namespace ResponsiBoss.BlazorServerApp
{
    public static class DateExtensions
    {
        public static string ToCalendarMonthString(this DateTime dateTime)
        {
            return dateTime.ToString("MMMM yyyy");
        }

        public static string ToCalendarWeekString(this DateTime dateTime)
        {
            var result = String.Empty;
            DateTime startDate = dateTime, endDate = dateTime;

            while (startDate.DayOfWeek != DayOfWeek.Sunday)
            {
                startDate = startDate.AddDays(-1);
            }

            while (endDate.DayOfWeek != DayOfWeek.Sunday)
            {
                endDate = endDate.AddDays(1);
            }

            result = startDate.Month == endDate.Month ? $"{startDate.ToString("dd")} - {endDate.ToString("dd MMM yyyy")}" :
                                                        $"{startDate.ToString("dd MMM")}  - {endDate.ToString("dd MMM yyyy")}";

            return result;
        }

        public static bool ShortDateStringEquals(this DateTime dateTime1, DateTime dateTime2)
        {
            return dateTime1.ToShortDateString().Equals(dateTime2.ToShortDateString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}