using System;

namespace DDDCinema.Scheduling
{
    public class Seance
    {
        private readonly TimeSpan CommercialsTime = TimeSpan.FromMinutes(30);
        private readonly TimeSpan CleaningTime = TimeSpan.FromMinutes(15);

        public Guid MovieId { get; private set; }
        public TimePeriod ShowTime { get; private set; }
        public TimePeriod TotalTime { get; private set; }

        public Seance(Movie movie, ClockTime startTime)
        {
            ClockTime endTime = startTime + CommercialsTime + movie.Length;
            ShowTime = new TimePeriod(startTime.Value, endTime.Value);
            TotalTime = ShowTime.ExtendWith(CleaningTime);
            MovieId = movie.Id;

            if (!TotalTime.IsWithin(OpeningHours.OfCinema))
            {
                throw new ArgumentException("Seance will not fit into opening hours");
            }
        }
    }
}
