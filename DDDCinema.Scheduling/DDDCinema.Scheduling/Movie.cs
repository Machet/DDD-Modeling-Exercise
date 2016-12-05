using System;

namespace DDDCinema.Scheduling
{
    public class Movie
    {
        public Guid Id { get; private set; }
        public TimeSpan Length { get; private set; }

        public Movie(Guid id, TimeSpan length)
        {
            Id = id;
            Length = length;
        }
    }
}
