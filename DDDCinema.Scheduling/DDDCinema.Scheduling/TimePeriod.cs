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
            if (start < TimeSpan.Zero)
            {
                throw new ArgumentException("Time Period could not start at negative value");
            }

            if (end > TimeSpan.FromHours(24))
            {
                throw new ArgumentException("Time Period could not end after day ends");
            }

            if (start > end)
            {
                throw new ArgumentException("Time period should not start after it ends");
            }

            Start = start;
            End = end;
        }

        public bool IsWithin(TimePeriod another)
        {
            return Start >= another.Start && End <= another.End;
        }

        public bool IsOverlapping(TimePeriod another)
        {
            return Start < another.End && End > another.Start;
        }

        public TimePeriod ExtendWith(TimeSpan timeSpan)
        {
            return new TimePeriod(Start, End + timeSpan);
        }

        public TimePeriod ExtendWith(TimePeriod another)
        {
            if (another.Start != End)
            {
                throw new InvalidOperationException("Does not start directly after");
            }

            return new TimePeriod(Start, another.End);
        }

        public bool Equals(TimePeriod another)
        {
            if (ReferenceEquals(another, null))
            {
                return false;
            }

            return End == another.End && Start == another.Start;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TimePeriod);
        }

        public override int GetHashCode()
        {
            return 13 * Start.GetHashCode() + End.GetHashCode();
        }

        public static bool operator ==(TimePeriod period1, TimePeriod period2)
        {
            return period1?.Equals(period2) ?? false;
        }

        public static bool operator !=(TimePeriod period1, TimePeriod period2)
        {
            return !(period1 == period2);
        }
    }
}
