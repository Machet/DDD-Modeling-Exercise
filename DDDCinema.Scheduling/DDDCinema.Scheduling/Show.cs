using System;

namespace DDDCinema.Scheduling
{
    public class Show
    {
        public TimePeriod ScheduledTime { get; private set; }
        public Guid MovieId { get; private set; }

        public Show(Movie movie, ClockTime time)
        {
            MovieId = movie.Id;
            ScheduledTime = new TimePeriod(time.Value, time.Value + movie.Length);
            ScheduledTime = ScheduledTime.ExtendWith(TimeSpan.FromMinutes(30));
            ScheduledTime = ScheduledTime.ExtendWith(TimeSpan.FromMinutes(15));

            if (!ScheduledTime.IsWithin(OpeningHours.OfCinema))
            {
                throw new InvalidOperationException();
            }
        }
    }
}