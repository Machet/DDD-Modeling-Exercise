using System;

namespace DDDCinema.Scheduling.Anemic
{
    public class Movie
    {
        public Guid Id { get; set; }
        public TimeSpan Length { get; set; }
    }
}
