using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDCinema.Scheduling
{
    public class Schedule
    {
        public int Id { get; set; }
        public WeekNumber WeekNumber { get; private set; }
        public Room Room { get; private set; }
        public List<Show> Shows { get; private set; }
        public bool IsApproved { get; private set; }

        public Schedule(WeekNumber week, Room room)
        {
            if(week == null) { throw new ArgumentNullException(); }
            if(room == null) { throw new ArgumentNullException(); }

            if (Week.Current.Number > week)
            {
                throw new ArgumentException("");
            }

            WeekNumber = week;
            Room = room;
            Shows = new List<Show>();
            IsApproved = false;
        }

        public void ScheduleMovie(Movie movie, ClockTime time)
        {
            if (IsApproved)
            {
                throw new InvalidOperationException();
            }

            if (Week.Current.Number > WeekNumber)
            {
                throw new InvalidOperationException("Schedule is closed");
            }

            var show = new Show(movie, time);

            if (Shows.Any(s => s.ScheduledTime.IsOverlapping(show.ScheduledTime)))
            {
                throw new InvalidOperationException("Show will overlap another");
            }

            Shows.Add(show);
        }

        public void Approve()
        {
            var totalTime = Shows.Sum(s => s.ScheduledTime.Length.Ticks);
            if (totalTime < OpeningHours.OfCinema.Length.Ticks / 2)
            {
                throw new InvalidOperationException("");
            }

            IsApproved = true;
        }
    }
}
