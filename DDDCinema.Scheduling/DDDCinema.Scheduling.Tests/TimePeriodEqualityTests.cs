using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DDDCinema.Scheduling.Tests
{
    [TestClass]
    public class TimePeriodEqualityTests
    {
        [TestMethod]
        public void TimePeriod_WhenComparingToNull_ThenItShouldNotBeEqual()
        {
            var timePeriod = new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15));

            timePeriod.Equals(null).ShouldBe(false);
        }

        [TestMethod]
        public void TimePeriod_WhenComparingToDifferentObject_ThenItShouldNotBeEqual()
        {
            var timePeriod = new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15));

            timePeriod.Equals(new TimeSpan(14)).ShouldBe(false);
        }

        [TestMethod]
        public void TimePeriod_WhenComparingToOneWithExactlyTheSameStartAndEnd_ThenItShouldBeEqual()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15));
            var timePeriod3 = new TimePeriod(TimeSpan.FromHours(13), TimeSpan.FromHours(15));

            timePeriod1.Equals(timePeriod2).ShouldBe(true);
            timePeriod1.Equals(timePeriod3).ShouldBe(false);

            (timePeriod1 == timePeriod2).ShouldBe(true);
            (timePeriod1 == timePeriod3).ShouldBe(false);

            (timePeriod1 != timePeriod2).ShouldBe(false);
            (timePeriod1 != timePeriod3).ShouldBe(true);
        }
    }
}
