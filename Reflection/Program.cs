using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Type Objekt
		Type p = typeof(Program); //typeof

		Program program = new();
		Type g = program.GetType(); //GetType()

		//Test per Reflection ausführen
		p.GetMethod("Test", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(program, null); //Nicht-statische Methode

		MethodInfo mi = p.GetMethod("Test", BindingFlags.NonPublic | BindingFlags.Static);
		mi.Invoke(null, ["Max"]); //Statische Methode

		p.GetProperty("Text").SetValue(program, "Hallo Welt");

		//Activator
		object program2 = Activator.CreateInstance(p); //Program Objekt erstellen über Activator

		//Assembly
		//Codeblock (Projekt)
		Assembly a = Assembly.GetExecutingAssembly(); //Das jetztige Projekt

		Assembly b = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2025_11_10\Events\bin\Debug\net9.0\Events.dll");

		Type dt = b.GetType("Events.Developer");

		object dev = Activator.CreateInstance(dt);

		dt.GetEvent("Start").AddEventHandler(dev, new Action(() => Console.WriteLine("Reflection Start")));
		dt.GetEvent("End").AddEventHandler(dev, new Action(() => Console.WriteLine("Reflection Ende")));
		dt.GetEvent("Progress").AddEventHandler(dev, new Action<int>(x => Console.WriteLine($"Reflection Progress: {x}")));

		dt.GetMethod("DoWork").Invoke(dev, null);
	}

	public string Text { get; set; }

	private void Test() => Console.WriteLine("Hello World");

	static void Test(string name) => Console.WriteLine($"Hallo {name}");
}
