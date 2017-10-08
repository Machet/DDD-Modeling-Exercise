using System.Collections.Generic;

namespace DDDCinema.Scheduling.Anemic
{
    public class Schedule
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public int RoomId { get; set; }
        public List<Show> Shows { get; set; }
        public bool IsApproved { get; set; }
    }
}
