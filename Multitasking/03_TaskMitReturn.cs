namespace Multitasking;

internal class _03_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Berechne);
		t.Start();

		bool hasPrinted = false;
		//Schleife wird erst gestartet, wenn das Result fertig ist
		for (int i = 0; i < 100; i++)
		{
			if (!hasPrinted && t.IsCompletedSuccessfully)
			{
				Console.WriteLine(t.Result); //Einfacher: await, ContinueWith
				hasPrinted = true;
			}
			Thread.Sleep(25);
			Console.WriteLine($"Main Thread: {i}");
		}

		Console.ReadKey();
	}

	static int Berechne()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}
}
