namespace DDDCinema.Scheduling;

internal class OpeningHours : TimePeriod
{
	public static OpeningHours OfCinema = new OpeningHours();

	public OpeningHours()
		: base(TimeSpan.FromHours(10), TimeSpan.FromHours(22))
	{
	}
}