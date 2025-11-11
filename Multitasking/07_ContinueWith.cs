namespace Multitasking;

internal class _07_ContinueWith
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.ContinueWith(vorherigerTask => Folgetask()); //Wenn t fertig ist, wird die Folgetask() Methode ausgeführt
		t.Start();

		/////////////////////////////////////////////////

		Task<int> t2 = new Task<int>(Berechne);
		t2.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Auch ein Ergebnis kann im ContinueWith angegriffen werden
		t2.Start();

		/////////////////////////////////////////////////

		Task t3 = new Task(Absturz);
		t3.ContinueWith(vorherigerTask =>
			Console.WriteLine(vorherigerTask.Exception != null ? vorherigerTask.Exception.StackTrace : "Alles in Ordnung"));
		t3.Start();

		/////////////////////////////////////////////////

		Task t4 = new Task(Run); //Wenn t4 fertig ist, werden bis zu 3 Folgetasks gestartet
		t4.ContinueWith(x => Console.WriteLine("Fertig"));
		t4.ContinueWith(x => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion);
		t4.ContinueWith(x => Console.WriteLine($"Fehler: {x.Exception.Message}"), TaskContinuationOptions.OnlyOnFaulted);
		t4.Start();

		Console.ReadKey();
	}

	static void Run()
	{
		Thread.Sleep(500);
		Console.WriteLine("Task 1 fertig");
	}

	static void Folgetask() => Console.WriteLine("Task 2 fertig");

	static int Berechne() => Random.Shared.Next();

	static void Absturz() => throw new NotImplementedException("Hallo");
}
