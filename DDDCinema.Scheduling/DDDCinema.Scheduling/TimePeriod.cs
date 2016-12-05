using System;

namespace DDDCinema.Scheduling
{
    public class TimePeriod
    {
        public TimeSpan Start { get; private set; }
        public TimeSpan End { get; private set; }
        public TimeSpan Length { get { return End - Start; } }

        public TimePeriod(TimeSpan start, TimeSpan end)
        {
        }

        public bool IsWithin(TimePeriod another)
        {
            return false;
        }

        public bool IsOverlapping(TimePeriod another)
        {
            return false;
        }

        public TimePeriod ExtendWith(TimeSpan timeSpan)
        {
            return null;
        }

        public TimePeriod ExtendWith(TimePeriod another)
        {
            return null;
        }

        public override bool Equals(object obj)
        {           
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(TimePeriod period1, TimePeriod period2)
        {
            return false;
        }

        public static bool operator !=(TimePeriod period1, TimePeriod period2)
        {
            return false;
        }
    }
}
