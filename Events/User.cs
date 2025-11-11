namespace Events;

internal class User
{
	static void Main(string[] args)
	{
		Developer d = new Developer();

		d.Start += D_Start;
		d.End += D_End;
		d.Progress += D_Progress;

		d.DoWork();
	}

	private static void D_Start()
	{
		Console.WriteLine("Prozess gestartet");
	}

	private static void D_End()
	{
		Console.WriteLine("Prozess beendet");
	}

	private static void D_Progress(int obj)
	{
		Console.WriteLine($"Fortschritt: {obj}");
	}
}
