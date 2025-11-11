namespace Multitasking;

internal class _04_TaskWarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		t.Wait();

		Task.WaitAll(t); //Wartet auf mehrere Tasks

		Task.WaitAny(t); //Wartet auf mehrere Tasks
	}

	static void Run()
	{

	}
}
