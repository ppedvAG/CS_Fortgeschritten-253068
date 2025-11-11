namespace Multitasking;

internal class _01_TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Task erstellen
		t.Start(); //Task starten

		//Ab hier parallel
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Task.Factory.StartNew(Run); //Ab .NET 4.0

		Task.Run(Run); //Ab .NET 4.5

		//Vordergrund und Hintergrundthreads
		//Wenn alle Vordergrundthreads beendet sind, werden alle Hintergrundthreads abgebrochen
		//Main Thread ist ein Vordergrundthread, Tasks sind immer Hintergrundthreads
		//Bei einer Konsolenanwendung muss das Programm blockiert werden
		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Task: {i}");
		}
	}
}
