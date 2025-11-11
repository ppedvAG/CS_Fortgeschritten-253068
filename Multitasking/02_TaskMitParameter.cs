namespace Multitasking;

internal class _02_TaskMitParameter
{
	static void Main(string[] args)
	{
		int x = 200;

		Task t = new Task(Run, x);
		t.Start();

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
			{
				Console.WriteLine($"Task: {i}");
			}
		}
	}
}
