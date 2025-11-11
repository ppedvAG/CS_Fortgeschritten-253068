namespace Multitasking;

internal class _05_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new(); //Source: Produziert CancellationTokens
		CancellationToken ct = cts.Token; //Generiert einen neuen Token aus der Source, welcher mit der Source verbunden ist

		Task t = new Task(Run, ct);
		t.Start();

		cts.CancelAfter(500); //Cancel-Signal an alle angehängten Token senden

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				//ct.ThrowIfCancellationRequested(); //Lässt das Programm nicht abstürzen

				if (ct.IsCancellationRequested)
				{
					Console.WriteLine("Task beendet");
					break;
				}

				Thread.Sleep(25);
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}
