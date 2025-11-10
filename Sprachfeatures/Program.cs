using IntList = System.Collections.Generic.List<int>;

namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		object o = 1;
		//if (o is int)
		//{
		//	int x = (int)o;
		//}

		if (o is int x) //Vererbungshierarchietypvergleich
		{
			//Muss auch bei Interfaces verwendet werden
		}

		if (o.GetType() == typeof(object)) //Genauer Typvergleich
		{

		}

		Test t = new Test();
		var (k, name, desc) = t;

		double d = 1_347_819_024.138_597;
		Console.WriteLine(d);

		//class und struct

		//class
		//Referenztyp
		//Wenn ein Objekt einer Klasse einer Variable zugewiesen wird, wird eine Referenz angelegt
		//Wenn zwei Objekte einer Klasse verglichen werden, werden die Speicheradresse verglichen
		Test t1 = new Test();
		Test t2 = t1; //Referenz auf t1 herstellen
		t1.ID = 10; //Beide Variablen haben jetzt den gleichen Wert

		Console.WriteLine(t1 == t2);
		Console.WriteLine(t1.GetHashCode() == t2.GetHashCode());

		//struct
		//Wertetyp
		//Wenn ein Objekt eines Structs einer Variable zugewiesen wird, wird eine Kopie erzeugt
		//Wenn zwei Objekte eines Structs verglichen werden, werden die Inhalte verglichen
		int i1 = 5;
		int i2 = i1;
		i1 = 10;

		//ref
		//Beliebige Typen referenzierbar machen
		int x1 = 5;
		ref int x2 = ref x1; //Zeiger auf die Zahl hinter x1
		x1 = 10; //Beide Werte werden verändert

		int zahl = 4;
		string z = zahl switch
		{
			1 => "Eins",
			2 => "Zwei",
			3 => "Drei",
			_ => ""
		};

		int[] zahlen = [1, 2, 3, 4, 5];
		//Console.WriteLine(zahlen[..6]);

		List<int> l = null;
		if (l == null)
			l = new List<int>();

		l = l == null ? new List<int>() : l;

		l = l ?? new List<int>(); //Wenn links (l) nicht null ist, nimm die Linke Seite, sonst die rechte Seite

		l ??= new List<int>(); //Seit C# 8

		Console.WriteLine($"Hallo Welt: {zahl}");
		Console.WriteLine(@"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\");

		List<int> n = new();
		var m = new	List<int>();

		switch (DateTime.Now.DayOfWeek)
		{
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
				Console.WriteLine("Wochentag");
				break;

			case DayOfWeek.Saturday or DayOfWeek.Sunday:
				Console.WriteLine("Wochenende");
				break;
		}

		Person p = new Person(0, "Max", "Mustermann", 33);
		Console.WriteLine(p);

		int id;
		string vorname;
		string nachname;
		int alter;

		(id, vorname, nachname, alter) = p;

		IntList list = [];

		Point point = new Point(3, 4);
		if (point is not null)
		{
			//...
		}

		int g = 5;
		double h = g;

		var v = new { Zahl = z, Int = g, Person = p };
	}

	void Test() => Console.WriteLine("Hallo");

	void Test2(string x) //https://github.com/dotnet/csharplang/blob/main/meetings/2022/LDM-2022-04-06.md#parameter-null-checking
	{

	}
}

public class Test
{
	public int ID;

	public string Name;

	public string Description;

	public void Deconstruct(out int ID, out string Name, out string Description)
	{
		ID = this.ID;
		Name = this.Name;
		Description = this.Description;
	}
}

public record Person(int ID, string Vorname, string Nachname, int Alter)
{
	public void Test()
	{
		//...
	}
}

public class Point(int x, int y)
{
	public int X = x;

	public int Y = y;

	public static bool operator ==(Point a, Point b)
	{
		return a.X == b.X && a.Y == b.Y;
	}

	public static bool operator !=(Point a, Point b)
	{	
		return !(a == b);
	}
}