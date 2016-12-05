using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDCinema.Scheduling
{
    public class RoomSchedule
    {
        public int RoomNumber { get; private set; }
        public WeekNumber WeekNumber { get; private set; }
        public bool IsSubmited { get; private set; }
        public List<Seance> Seances { get; private set; }

        public RoomSchedule(Room room, WeekNumber week)
        {
            RoomNumber = room.Number;
            WeekNumber = week;
            Seances = new List<Seance>();
        }

        public void ScheduleMovie(Movie movie, ClockTime time)
        {
            if (IsSubmited)
            {
                throw new InvalidOperationException("Could not schedule movie on submited schedule");
            }

            if (WeekNumber <= Week.Current.Number)
            {
                throw new InvalidOperationException("Could not schedule movie for past weeks");
            }

            var seance = new Seance(movie, time);
            if (Seances.Any(s => s.TotalTime.IsOverlapping(seance.TotalTime)))
            {
                throw new InvalidOperationException("Seance will overlap another");
            }

            Seances.Add(seance);
        }

        public void Submit()
        {
            if (IsSubmited)
            {
                return;
            }

            var totalBookedMinutes = Seances.Sum(s => s.TotalTime.Length.TotalMinutes);
            var minimumBookedMinutes = OpeningHours.OfCinema.Length.TotalMinutes / 2;
            if (minimumBookedMinutes > totalBookedMinutes)
            {
                throw new InvalidOperationException("Could not submit schedule - more bookings needed");
            }

            IsSubmited = true;
        }
    }
}

