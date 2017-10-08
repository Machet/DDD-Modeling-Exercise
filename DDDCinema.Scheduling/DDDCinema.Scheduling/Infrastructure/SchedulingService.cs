using System;

namespace DDDCinema.Scheduling
{
    public class SchedulingService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IRoomRepository _roomRepository;

        public SchedulingService(IScheduleRepository scheduleRepository, IMovieRepository movieRepository, IRoomRepository roomRepository)
        {
            _scheduleRepository = scheduleRepository;
            _movieRepository = movieRepository;
            _roomRepository = roomRepository;
        }

        public int Create(int week, int roomId)
        {
            Room room = _roomRepository.Get(roomId);
            Schedule schedule = new Schedule(new WeekNumber(week), room);

            _scheduleRepository.Add(schedule);
            return schedule.Id;
        }

        public void AddMovieToSchedule(int scheduleId, int movieId, TimeSpan time)
        {
            Schedule schedule = GetSchedule(scheduleId);
            Movie movie = GetMovie(movieId);

            schedule.ScheduleMovie(movie, new ClockTime(time));

            _scheduleRepository.Save();
        }

        public void ApproveSchedule(int scheduleId)
        {
            Schedule schedule = GetSchedule(scheduleId);
            schedule.Approve();

            _scheduleRepository.Save();
        }

        private Schedule GetSchedule(int scheduleId)
        {
            Schedule schedule = _scheduleRepository.Get(scheduleId);
            if (schedule == null)
            {
                throw new InvalidOperationException($"Schedule with id {scheduleId} doesnt exist");
            }

            return schedule;
        }

        private Movie GetMovie(int movieId)
        {
            Movie movie = _movieRepository.Get(movieId);
            if (movie == null)
            {
                throw new InvalidOperationException($"Movie with id {movieId} doesnt exist");
            }

            return movie;
        }
    }
}
