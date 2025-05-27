namespace DDDCinema.Scheduling;

public class WeekNumber
{
	public int Value { get; private set; }

	public WeekNumber(int week)
	{
		if (week < 0 || week > 53)
		{
			throw new ArgumentException("Its not a week number");
		}

		Value = week;
	}

	public override bool Equals(object obj)
	{
		var another = obj as WeekNumber;
		if (obj == null)
		{
			return false;
		}

		return this == another;
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	public static bool operator ==(WeekNumber weekNumber1, WeekNumber weekNumber2)
	{
		return weekNumber1.Value == weekNumber2.Value;
	}

	public static bool operator !=(WeekNumber weekNumber1, WeekNumber weekNumber2)
	{
		return !(weekNumber1 == weekNumber2);
	}

	public static bool operator <(WeekNumber weekNumber1, WeekNumber weekNumber2)
	{
		return weekNumber1.Value < weekNumber2.Value;
	}

	public static bool operator >(WeekNumber weekNumber1, WeekNumber weekNumber2)
	{
		return weekNumber1.Value > weekNumber2.Value;
	}

	public static bool operator <=(WeekNumber weekNumber1, WeekNumber weekNumber2)
	{
		return weekNumber1.Value <= weekNumber2.Value;
	}

	public static bool operator >=(WeekNumber weekNumber1, WeekNumber weekNumber2)
	{
		return weekNumber1.Value >= weekNumber2.Value;
	}

	public static WeekNumber operator +(WeekNumber weekNumber, int number)
	{
		return new WeekNumber(weekNumber.Value + number);
	}

	public static WeekNumber operator -(WeekNumber weekNumber, int number)
	{
		return new WeekNumber(weekNumber.Value - number);
	}
}
