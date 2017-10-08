using System;

namespace DDDCinema.Scheduling.Anemic
{
    public class Show
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MovieId { get; set; }
    }
}