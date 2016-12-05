using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace DDDCinema.Scheduling.Tests
{
    [TestClass]
    public class TimePeriodTests
    {
        [TestMethod]
        public void TimePeriodOverlapping_WhenPeriodsShareCommonPart_ThenReturnsTrue()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(11), TimeSpan.FromHours(13));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(12), TimeSpan.FromHours(14));

            timePeriod1.IsOverlapping(timePeriod2).ShouldBe(true);
            timePeriod2.IsOverlapping(timePeriod1).ShouldBe(true);
        }

        [TestMethod]
        public void TimePeriodOverlapping_WhenPeriodIsWithinAnother_ThenReturnsTrue()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(10), TimeSpan.FromHours(16));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(12), TimeSpan.FromHours(14));

            timePeriod1.IsOverlapping(timePeriod2).ShouldBe(true);
            timePeriod2.IsOverlapping(timePeriod1).ShouldBe(true);
        }

        [TestMethod]
        public void TimePeriodOverlapping_WhenThereIsGapBetweenPeriods_ThenReturnsFalse()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(10), TimeSpan.FromHours(12));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(12), TimeSpan.FromHours(14));

            timePeriod1.IsOverlapping(timePeriod2).ShouldBe(false);
            timePeriod2.IsOverlapping(timePeriod1).ShouldBe(false);
        }

        [TestMethod]
        public void TimePeriodWithinCheck_WhenOnePeriodContainsAnother_ThenReturnsTrue()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(10), TimeSpan.FromHours(12));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(8), TimeSpan.FromHours(14));

            timePeriod1.IsWithin(timePeriod2).ShouldBe(true);
            timePeriod2.IsWithin(timePeriod1).ShouldBe(false);
        }

        [TestMethod]
        public void TimePeriodWithinCheck_WhenPeriodContainsPartOfAnother_ThenReturnsFalse()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(10), TimeSpan.FromHours(14));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(8), TimeSpan.FromHours(12));

            timePeriod1.IsWithin(timePeriod2).ShouldBe(false);
            timePeriod2.IsWithin(timePeriod1).ShouldBe(false);
        }

        [TestMethod]
        public void TimePeriodWithinCheck_WhenPeriodsDoesntHaveCommonPart_ThenReturnsFalse()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(12), TimeSpan.FromHours(14));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(8), TimeSpan.FromHours(10));

            timePeriod1.IsWithin(timePeriod2).ShouldBe(false);
            timePeriod2.IsWithin(timePeriod1).ShouldBe(false);
        }
    }
}
