namespace Multitasking;

internal class _08_Lock
{
	public static int Counter { get; set; }

	public static readonly object Lock = new();

	static void Main(string[] args)
	{
		List<Task> tasks = new List<Task>();
		for (int i = 0; i < 100; i++)
			tasks.Add(Task.Run(Increment100));
		Console.ReadKey();
	}

	static void Increment100()
	{
		for (int i = 0; i < 100; i++)
		{
			lock (Lock) //Jetzt darf nur ein Task gleichzeitig auf diesen Block zugreifen
			{
				Counter++;
				Console.WriteLine(Counter);
			}

			Monitor.Enter(Lock); //Jetzt darf nur ein Task gleichzeitig auf diesen Block zugreifen
			Counter++;
			Console.WriteLine(Counter);
			Monitor.Exit(Lock);

			//Interlocked.Add(ref Counter, 1);
		}
	}
}
