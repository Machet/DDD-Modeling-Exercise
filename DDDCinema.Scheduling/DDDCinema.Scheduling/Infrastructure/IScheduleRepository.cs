namespace DDDCinema.Scheduling
{
    public interface IScheduleRepository
    {
        void Add(Schedule schedule);
        Schedule Get(int scheduleId);
        void Save();
    }
}
