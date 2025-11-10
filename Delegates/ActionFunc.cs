using System.Threading.Channels;

namespace Delegates;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action und Func
		//Zwei vorgegebene Delegates, welche an vielen Stellen in C# verwendet
		//z.B. TPL, async/await, Linq, Reflection, ...

		//Action
		//Methodenzeiger, welcher immer void zurückgibt, und bis zu 16 Parameter hat
		Action a = HelloWorld;
		a();
		a?.Invoke();

		Action<int, int> addiere = Addiere;
		addiere?.Invoke(4, 5);

		//Praktisches Beispiel
		Task t = new Task(Task, 1);

		List<int> ints = [1, 2, 3, 4, 5];
		ints.ForEach(PrintListe);

		//Func
		//Methodenzeiger, welcher einen konkreten Rückgabewert hat, und bis zu 16 Parameter hat
		//Der Rückgabewert ist immer das letzte Generic
		Func<int> randomZahl = RandomZahl;
		Console.WriteLine(randomZahl?.Invoke());

		Func<int, int, double> subtrahiere = Subtrahiere;
		Console.WriteLine(subtrahiere?.Invoke(8, 4));

		//double diff = subtrahiere?.Invoke(8, 4);
		double? diff2 = subtrahiere?.Invoke(8, 4);
		if (diff2 != null)
		{
			double diff3 = diff2.Value;
		}
		double diff4 = subtrahiere?.Invoke(8, 4) ?? double.NaN;

		//Praktisches Beispiel
		ints.Where(TeilbarDurch2);

		//Anonyme Funktionen: Methodenzeiger, welche nicht dediziert erstellt werden, sondern nur bei dem Action-/Funcparameter eingesetzt werden
		//-> werden einmal verwendet und weggeworfen

		Func<int, int, double> div;
		div = delegate (int x, int y)
		{
			return (double) x / y;
		};

		div += (int x, int y) =>
		{
			return (double) x / y;
		};

		div += (int x, int y) => (double) x / y;

		div += (x, y) => (double) x / y;

		//Where mit Lambda
		ints.Where(e => e % 2 == 0);

		//ForEach mit Lambda
		ints.ForEach(Console.WriteLine);

		string text = "Hallo Welt";
		text.All(char.IsLetter); //char.IsLetter passt hier genau hinein
	}

	#region Action
	static void HelloWorld() => Console.WriteLine("Hello World");

	static void Addiere(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");

	static void Task(object? o) { }

	static void PrintListe(int i) => Console.WriteLine(i);
	#endregion

	#region Func
	static int RandomZahl() => Random.Shared.Next();

	static double Subtrahiere(int x, int y) => x - y;

	static bool TeilbarDurch2(int x) => x % 2 == 0;
	#endregion
}
