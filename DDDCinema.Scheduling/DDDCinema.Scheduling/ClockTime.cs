using System;

namespace DDDCinema.Scheduling
{
    public class ClockTime
    {
        public TimeSpan Value { get; private set; }

        public ClockTime(int hours, int minutes)
            :this(new TimeSpan(hours, minutes, 0))
        {            
        }

        public ClockTime(TimeSpan time)
        {
            if(time >= TimeSpan.FromHours(24) || time < TimeSpan.Zero)
            {
                throw new ArgumentException("Invalid Value for clock time");
            }

            Value = time;
        }

        public override bool Equals(object obj)
        {
            var another = obj as ClockTime;
            if (obj == null)
            {
                return false;
            }

            return this == another;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static ClockTime operator +(ClockTime atTime, TimeSpan time)
        {
            return new ClockTime(atTime.Value + time);
        }

        public static bool operator ==(ClockTime time1, ClockTime time2)
        {
            return time1.Value == time2.Value;
        }

        public static bool operator !=(ClockTime time1, ClockTime time2)
        {
            return time1.Value != time2.Value;
        }

        public static bool operator <(ClockTime time1, ClockTime time2)
        {
            return time1.Value < time2.Value;
        }

        public static bool operator >(ClockTime time1, ClockTime time2)
        {
            return time1.Value < time2.Value;
        }
    }
}