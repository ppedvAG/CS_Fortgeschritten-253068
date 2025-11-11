namespace Multitasking;

internal class _06_Exceptions
{
	static void Main(string[] args)
	{
		Task t = new Task(Run);
		t.Start();

		//AggregateException wird an 3 Stellen ausgegeben
		//- t.Result
		//- t.Wait()
		//- Task.WaitAll

		try
		{
			t.Wait();
		}
		catch (AggregateException ex)
		{
			foreach (Exception e in ex.InnerExceptions)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}

		Console.ReadKey();
	}

	static void Run()
	{
		Thread.Sleep(500);
		throw new FieldAccessException("Hallo Welt");
	}
}
