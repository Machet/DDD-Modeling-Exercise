using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace DDDCinema.Scheduling.Tests
{
    [TestClass]
    public class MovieSchedulingTests
    {
        private object schedule;

        [TestMethod]
        public void SchedulingMovie_WhenScheduleIsMadeForPassedWeek_ThenExceptionIsThrown()
        {
            CreateSchedule(Week.Current.Number - 1);

            Should.Throw<InvalidOperationException>(() => 
                ScheduleMovie(MovieWithLength(120), new ClockTime(12, 00)));
        }

        [TestMethod]
        public void SchedulingMovie_WhenItCollidesWithExistingSchedules_ThenExceptionIsThrown()
        {
            CreateSchedule();
            ScheduleMovie(MovieWithLength(120), new ClockTime(12, 00));

            Should.Throw<InvalidOperationException>(() => 
                ScheduleMovie(MovieWithLength(120), new ClockTime(13, 00)));

            Should.Throw<InvalidOperationException>(() =>
                ScheduleMovie(MovieWithLength(120), new ClockTime(11, 00)));

            Should.NotThrow(() =>
                ScheduleMovie(MovieWithLength(120), new ClockTime(16, 00)));
        }

        [TestMethod]
        public void SchedulingMovie_WhenScheduleIsMade_ThenCleaningAndCommercialsAreIncluded()
        {
            CreateSchedule();
            ScheduleMovie(MovieWithLength(120), new ClockTime(12, 00));

            TotalLengthOfMovie(schedule).ShouldBe(TimeSpan.FromMinutes(120 + 15 + 30));
        }

        [TestMethod]
        public void SchedulingMovie_WhenScheduledMovieExceedsOpeningHours_ThenExceptionIsThrown()
        {
            CreateSchedule();

            Should.Throw<ArgumentException>(() =>
                ScheduleMovie(MovieWithLength(120), new ClockTime(21, 00)));

            Should.Throw<ArgumentException>(() =>
                ScheduleMovie(MovieWithLength(120), new ClockTime(9, 00)));

            Should.NotThrow(() =>
                ScheduleMovie(MovieWithLength(120), new ClockTime(16, 00)));
        }

        [TestMethod]
        public void SchedulingMovie_WhenScheduleIsSubmited_ThenItCannotBeChanged()
        {
            CreateSchedule();
            ScheduleMovie(MovieWithLength(360), new ClockTime(10, 00));
            Submit();

            Should.Throw<InvalidOperationException>(() =>
                ScheduleMovie(MovieWithLength(120), new ClockTime(19, 00)));
        }

        [TestMethod]
        public void SubmittingSchedule_WhenSeancesDoesntCoverAtLeastHalfOfADay_ThenExceptionIsThrown()
        {
            CreateSchedule();
            ScheduleMovie(MovieWithLength(120), new ClockTime(10, 00));

            Should.Throw<InvalidOperationException>(() =>
                Submit());

            ScheduleMovie(MovieWithLength(300), new ClockTime(15, 00));

            Should.NotThrow(() =>
                Submit());
        }

        [TestMethod]
        public void SubmittingSchedule_WhenThereIsEnoughSeances_ThenScheduleIsMarkedAsSubmitted()
        {
            CreateSchedule();
            ScheduleMovie(MovieWithLength(120), new ClockTime(10, 00));

            Should.Throw<InvalidOperationException>(() =>
                Submit());

            ScheduleMovie(MovieWithLength(300), new ClockTime(15, 00));
            Submit();

            IsSubmited(schedule).ShouldBe(true);
        }

        private void CreateSchedule(WeekNumber weekNumber = null)
        {
            var room = new Room(1);
            var week = weekNumber ?? Week.Current.Number + 1;
            schedule = null;
        }

        private void ScheduleMovie(Movie movie, ClockTime clockTime)
        {
        }

        private void Submit()
        {
        }

        private bool IsSubmited(object schedule)
        {
            return false;
        }

        private TimeSpan TotalLengthOfMovie(object schedule)
        {
            return TimeSpan.Zero;
        }

        private static Movie MovieWithLength(int length)
        {
            return new Movie(Guid.NewGuid(), TimeSpan.FromMinutes(length));
        }
    }
}
