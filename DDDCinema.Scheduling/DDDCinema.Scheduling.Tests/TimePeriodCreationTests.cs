using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DDDCinema.Scheduling.Tests
{
    [TestClass]
    public class TimePeriodCreationTests
    {
        [TestMethod]
        public void CreatingTimePeriod_WhenStartTimeNegative_ThenShouldThrowException()
        {
            Should.Throw<ArgumentException>(() => new TimePeriod(TimeSpan.FromHours(-14), TimeSpan.FromHours(15)));
            Should.Throw<ArgumentException>(() => new TimePeriod(TimeSpan.FromMinutes(-1), TimeSpan.FromHours(15)));
            Should.NotThrow(() => new TimePeriod(TimeSpan.FromHours(0), TimeSpan.FromHours(15)));
        }

        [TestMethod]
        public void CreatingTimePeriod_WhenEndTimeExceeds24H_ThenShouldThrowException()
        {
            Should.Throw<ArgumentException>(() => new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(25)));
            Should.Throw<ArgumentException>(() => new TimePeriod(TimeSpan.FromHours(14), new TimeSpan(24, 0, 1)));
            Should.NotThrow(() => new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(24)));
        }

        [TestMethod]
        public void CreatingTimePeriod_WhenStartTimeHigherThanEnd_ThenShouldThrowException()
        {
            Should.Throw<ArgumentException>(() => new TimePeriod(TimeSpan.FromHours(15), TimeSpan.FromHours(14)));
            Should.NotThrow(() => new TimePeriod(TimeSpan.FromHours(15), TimeSpan.FromHours(15)));
            Should.NotThrow(() => new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15)));
        }

        [TestMethod]
        public void CreatingTimePeriod_WhenCorectValuesAreProvided_ThenStartAndEndAreSet()
        {
            var timePeriod = new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15));

            timePeriod.Start.ShouldBe(TimeSpan.FromHours(14));
            timePeriod.End.ShouldBe(TimeSpan.FromHours(15));
        }
    }
}
