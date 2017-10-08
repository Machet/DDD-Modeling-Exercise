using System;

namespace DDDCinema.Scheduling.Anemic
{
    public class TimeUtils
    {
        public static bool AreOverlapping(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2)
        {
            AssertCorrectTime(startTime1, endTime1);
            AssertCorrectTime(startTime2, endTime2);
            return startTime1 < endTime2 && endTime1 > startTime2;
        }

        public static bool AreWithin(TimeSpan startTime1, TimeSpan endTime1, TimeSpan startTime2, TimeSpan endTime2)
        {
            AssertCorrectTime(startTime1, endTime1);
            AssertCorrectTime(startTime2, endTime2);
            return startTime1 >= startTime2 && endTime1 <= endTime2;
        }

        private static void AssertCorrectTime(TimeSpan start, TimeSpan end)
        {
            if (start < TimeSpan.Zero)
            {
                throw new ArgumentException("time cannot have negative value");
            }

            if (end > TimeSpan.FromHours(24))
            {
                throw new ArgumentException("Time could not end after day ends");
            }

            if (start > end)
            {
                throw new ArgumentException("start must be before end");
            }
        }
    }
}
