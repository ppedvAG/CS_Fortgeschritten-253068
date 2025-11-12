using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		//IEnumerable
		//Nur eine Anleitung zur Erstellung der Daten
		//Hält selbst keine Daten
		IEnumerable<int> zahlen = Enumerable.Range(0, 1_000_000_000); //1ms

		//Anleitung ausführen
		//List<int> zahlen2 = Enumerable.Range(0, 1_000_000_000).ToList(); //1s

		//Enumerator
		List<int> ints = [1, 2, 3, 4, 5];
		foreach (int f in ints)
		{
			Console.WriteLine(f);
		}

		//foreach ohne foreach
		IEnumerator<int> e = ints.GetEnumerator();
		e.MoveNext();
		start:
		Console.WriteLine(e.Current);
		if (e.MoveNext())
			goto start; //goto nicht verwenden
		e.Reset();

		///////////////////////////////////////////////////////

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		///////////////////////////////////////////////////////

		//Erweiterungsmethoden
		//Methoden, welche an beliebige Typen angehängt werden können
		int x = 123;

		Console.WriteLine(x.Quersumme());

		Console.WriteLine(1274.Quersumme());

		//Vom Compiler generiert
		ExtensionMethods.Quersumme(x);

		//Eigene Linq Methoden
		ints.ForEach(x => Console.WriteLine(x));

		int[] i = [1, 2, 3, 4, 5];
		i.ForEach(x => Console.WriteLine(x));

		Dictionary<string, int> dict = [];
		dict.ForEach(x => Console.WriteLine($"Key: {x.Key}, Value: {x.Value}"));

		fahrzeuge.Shuffle();

		string pfad = @"C:\Users\lk3\Unterlagen\PPKURS-CS-Fortgeschritten\M-009-CS-Fortgeschritten TPL-AsyncAwait\M-009 LabCode (Teilnehmern zur Verfügung stellen)\M-008 LabCode\history.city.list.min.json";
		string json = File.ReadAllText(pfad);
		JsonDocument doc = JsonDocument.Parse(json);
		foreach (JsonElement element in doc.RootElement.EnumerateArray())
		{
			element.GetProperty<double>("city", "coord", "lon");
		}
	}
}

public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		//int summe = 0;
		//string zahlAlsString = x.ToString();
		//for (int i = 0; i < zahlAlsString.Length; i++)
		//{
		//	summe += (int) char.GetNumericValue(zahlAlsString[i]);
		//}
		//return summe;

		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
	{
		foreach (T value in values)
			action(value);
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> values) => values.OrderBy(e => Random.Shared.Next());

	public static JsonElement GetProperty(this JsonElement element, params string[] names)
	{
		JsonElement current = element;
		foreach (string name in names)
		{
			current = current.GetProperty(name);
		}
		return current;
	}

	public static T GetProperty<T>(this JsonElement element, params string[] names)
	{
		JsonElement current = element;
		foreach (string name in names)
		{
			current = current.GetProperty(name);
		}

		object x = default(T) switch
		{
			bool => current.GetBoolean(),
			sbyte => current.GetSByte(),
			byte => current.GetByte(),
			short => current.GetInt16(),
			ushort => current.GetUInt16(),
			int => current.GetInt32(),
			uint => current.GetUInt32(),
			long => current.GetInt64(),
			ulong => current.GetUInt64(),
			double => current.GetDouble(),
			float => current.GetSingle(),
			decimal => current.GetDecimal(),
			DateTime => current.GetDateTime(),
			DateTimeOffset => current.GetDateTimeOffset(),
			null => current.GetRawText(), //Sonderfall: string
			_ => throw new Exception("Unbekannter Typ")
		};
		return (T) x;
	}
}

[DebuggerDisplay("MaxV: {MaxV}, Marke: {Marke}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }