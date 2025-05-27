using System.Globalization;

namespace DDDCinema.Scheduling;

public abstract class Week
{
	public abstract WeekNumber Number { get; }
	public static Week Current { get; private set; }

	static Week()
	{
		Current = new DefaultWeek();
	}

	public static void SetCurrentWeekProvider(Week provider)
	{
		Current = provider;
	}

	private class DefaultWeek : Week
	{
		public override WeekNumber Number
		{
			get
			{
				var weekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
				return new WeekNumber(weekNumber);
			}
		}
	}
}
