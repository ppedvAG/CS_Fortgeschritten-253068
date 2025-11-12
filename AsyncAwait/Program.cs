using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		///////////////////////////////////////////////////////

		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Tasse);
		//t2.Wait(); //Sollte nicht verwendet werden
		//Task t3 = Task.Run(Kaffee);
		//Task.WaitAll(t1, t3); //Sollte nicht verwendet werden
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//Wait() und WaitAll(...) sollten nicht verwenden werden

		///////////////////////////////////////////////////////

		//Task t1 = new Task(Toast);
		//t1.ContinueWith(x => Console.WriteLine(sw.ElapsedMilliseconds));
		//t1.Start();
		//Task t2 = new Task(Tasse);
		//t2.ContinueWith(x => Kaffee());
		//t2.Start();

		//Sehr komplex mit ContinueWith um Wait/WaitAll zu sparen

		///////////////////////////////////////////////////////

		//Task t1 = ToastAsync(); //WICHTIG: Kein Task.Run() notwendig, weil async Task Methoden automatisch starten
		//Task t2 = TasseAsync();
		//await t2; //Warte hier auf t2
		//Task t3 = KaffeeAsync();
		//await Task.WhenAll(t1, t3); //WhenAll: await für mehrere Tasks
		//Console.WriteLine(sw.ElapsedMilliseconds);

		///////////////////////////////////////////////////////

		//Task<Toast> t1 = ToastObjectAsync(); //Starte den Toast
		//Task<Tasse> t2 = TasseObjectAsync(); //Starte die Tasse
		//Tasse tasse = await t2; //Warte auf die Tasse
		//Task<Kaffee> t3 = KaffeeObjectAsync(tasse); //Starte den Kaffee
		//Toast toast = await t1; //Warte auf den Toast
		//Kaffee kaffee = await t3; //Warte auf den Kaffee
		//Fruehstueck f = new Fruehstueck(toast, kaffee);

		///////////////////////////////////////////////////////

		//Kompakte Schreibweise
		Task<Toast> t1 = ToastObjectAsync(); //Starte den Toast
		Task<Tasse> t2 = TasseObjectAsync(); //Starte die Tasse
		Task<Kaffee> t3 = KaffeeObjectAsync(await t2); //Starte den Kaffee
		Fruehstueck f = new Fruehstueck(await t1, await t3);

		Console.ReadKey();
	}

	#region Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	/// <summary>
	/// void == async Task
	/// Toast() == ToastAsync(), aber diese Methode kann awaited werden
	/// </summary>
	static async Task ToastAsync()
	{
		await Task.Delay(4000); //await Task.Run(() => Thread.Sleep(4000));
		Console.WriteLine("Toast fertig");
	}

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron mit Objekten
	static async Task<Toast> ToastObjectAsync()
	{
		await Task.Delay(4000); //await Task.Run(() => Thread.Sleep(4000));
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public record Toast;

public record Tasse;

public record Kaffee(Tasse t);

public record Fruehstueck(Toast t, Kaffee k);