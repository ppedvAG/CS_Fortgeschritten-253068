namespace Events;

internal class Program
{
	public event EventHandler TestEvent; //Definition von einem Event (Entwicklerseite)

	public event EventHandler<int> IntEvent;

	private event EventHandler accessorEvent;

	public event EventHandler AccessorEvent
	{
		add
		{
			accessorEvent += value;
			Console.WriteLine($"Event angehängt: {value.Method.Name}");
		}
		remove
		{
			accessorEvent -= value;
			Console.WriteLine($"Event abgehängt: {value.Method.Name}");
		}
	}

	static void Main(string[] args) => new Program();

	public Program()
	{
		TestEvent += Program_TestEvent; //Methode anhängen (Anwenderseite)
		TestEvent?.Invoke(this, EventArgs.Empty); //Event ausführen ohne Daten (Entwicklerseite)

		IntEvent += Program_IntEvent;
		IntEvent?.Invoke(this, 123);

		AccessorEvent += Program_AccessorEvent;
	}

	/// <summary>
	/// Anwenderseite
	/// 
	/// In sender in das Objekt hinterlegt, welches das Event ausgeführt hat
	/// In e befinden sich die Daten, welche beim Ausführen werden können
	/// </summary>
	private void Program_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("TestEvent ausgeführt");
	}

	private void Program_IntEvent(object? sender, int e)
	{
		Console.WriteLine($"Die Zahl: {e}");
	}

	private void Program_AccessorEvent(object? sender, EventArgs e)
	{
		throw new NotImplementedException();
	}
}
