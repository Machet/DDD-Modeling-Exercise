using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace DDDCinema.Scheduling.Tests
{
    [TestClass]
    public class TimePeriodExtendingTests
    {
        [TestMethod]
        public void ExtendingTimePeriod_WhenExtendingWithTime_ThenEndShouldBeMovedAboutSpecifiedTime()
        {
            var timePeriod = new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15));

            var result = timePeriod.ExtendWith(TimeSpan.FromHours(3));

            timePeriod.Start.ShouldBe(TimeSpan.FromHours(14));
            timePeriod.End.ShouldBe(TimeSpan.FromHours(15));

            result.Start.ShouldBe(TimeSpan.FromHours(14));
            result.End.ShouldBe(TimeSpan.FromHours(18));
        }

        [TestMethod]
        public void ExtendingTimePeriod_WhenExtendingWithNegativeTime_ThenExceptionIsThrown()
        {
            var timePeriod = new TimePeriod(TimeSpan.FromHours(14), TimeSpan.FromHours(15));

            Should.Throw<ArgumentException>(() => timePeriod.ExtendWith(TimeSpan.FromHours(-3)));
        }

        [TestMethod]
        public void ExtendingTimePeriod_WhenExtendWithAnotherTimePeriodThatStartsAfterFirst_ThenPeriodsShouldBeMerged()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(10), TimeSpan.FromHours(12));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(12), TimeSpan.FromHours(15));

            var result = timePeriod1.ExtendWith(timePeriod2);
            
            result.Start.ShouldBe(TimeSpan.FromHours(10));
            result.End.ShouldBe(TimeSpan.FromHours(15));

            timePeriod1.Start.ShouldBe(TimeSpan.FromHours(10));
            timePeriod1.End.ShouldBe(TimeSpan.FromHours(12));

            timePeriod2.Start.ShouldBe(TimeSpan.FromHours(12));
            timePeriod2.End.ShouldBe(TimeSpan.FromHours(15));
        }

        [TestMethod]
        public void ExtendingTimePeriod_WhenThereIsEmptySpaceBetweenTimePeriods_ThenExceptionIsThrown()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(10), TimeSpan.FromHours(12));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(13), TimeSpan.FromHours(14));

            Should.Throw<InvalidOperationException>(() => timePeriod1.ExtendWith(timePeriod2));
        }

        [TestMethod]
        public void ExtendingTimePeriod_WhenPeriodEndsBeforeEndOfExtendedPeriod_ThenExceptionIsThrown()
        {
            var timePeriod1 = new TimePeriod(TimeSpan.FromHours(10), TimeSpan.FromHours(12));
            var timePeriod2 = new TimePeriod(TimeSpan.FromHours(12), TimeSpan.FromHours(14));

            Should.Throw<InvalidOperationException>(() => timePeriod2.ExtendWith(timePeriod1));
        }
    }
}
