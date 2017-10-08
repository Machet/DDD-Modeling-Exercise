using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDCinema.Scheduling.Anemic
{
    public class SchedulingService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ICurrentWeekProvider _currentWeekProvider;

        public SchedulingService(IScheduleRepository scheduleRepository, IMovieRepository movieRepository, ICurrentWeekProvider currentWeekProvider)
        {
            _scheduleRepository = scheduleRepository;
            _movieRepository = movieRepository;
            _currentWeekProvider = currentWeekProvider;
        }

        public int Create(int week, int roomId)
        {
            if (_currentWeekProvider.Get() > week)
            {
                throw new ArgumentException("Cannot create schedule in past");
            }

            var schedule = new Schedule
            {
                WeekNumber = week,
                RoomId = roomId,
                Shows = new List<Show>(),
            };

            _scheduleRepository.Add(schedule);
            return schedule.Id;
        }

        public void AddMovieToSchedule(int scheduleId, int movieId, TimeSpan time)
        {
            Schedule schedule = GetSchedule(scheduleId);

            if (schedule.IsApproved)
            {
                throw new InvalidOperationException("Cannot change approved schedule");
            }

            if (_currentWeekProvider.Get() > schedule.WeekNumber)
            {
                throw new InvalidOperationException("Schedule is closed");
            }

            Movie movie = GetMovie(movieId);

            var endDate = time + movie.Length + Constants.CleaningTime + Constants.ComercialsTime;

            var show = new Show
            {
                MovieId = movieId,
                StartTime = time,
                EndTime = endDate
            };

            ValidateShow(show, schedule);
            schedule.Shows.Add(show);

            _scheduleRepository.Save();
        }

        public void ApproveSchedule(int scheduleId)
        {
            Schedule schedule = GetSchedule(scheduleId);

            var totalTime = schedule.Shows.Sum(s => (s.EndTime - s.StartTime).Ticks);
            var openingTime = (Constants.CinemaClosing - Constants.CinemaOpening).Ticks;
            if (totalTime < openingTime / 2)
            {
                throw new InvalidOperationException("Schedule must cover at least half of opening hours");
            }

            schedule.IsApproved = true;

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

        private void ValidateShow(Show show, Schedule schedule)
        {
            if (TimeUtils.AreWithin(show.StartTime, show.EndTime, Constants.CinemaOpening, Constants.CinemaClosing))
            {
                throw new InvalidOperationException("Show doesn't match opening hours of cinema");
            }

            if (schedule.Shows.Any(s => TimeUtils.AreOverlapping(show.StartTime, show.EndTime, s.StartTime, s.EndTime)))
            {
                throw new InvalidOperationException("Show is overlapping with another");
            }
        }
    }
}
