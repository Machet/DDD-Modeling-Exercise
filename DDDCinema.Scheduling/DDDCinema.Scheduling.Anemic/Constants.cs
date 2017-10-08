using System;

namespace DDDCinema.Scheduling.Anemic
{
    public class Constants
    {
        public static readonly TimeSpan CinemaOpening = TimeSpan.FromHours(10);
        public static readonly TimeSpan CinemaClosing = TimeSpan.FromHours(22);
        public static readonly TimeSpan CleaningTime = TimeSpan.FromMinutes(15);
        public static readonly TimeSpan ComercialsTime = TimeSpan.FromMinutes(30);
    }
}