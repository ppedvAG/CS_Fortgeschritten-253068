internal partial class Program
{
	private static void Main(string[] args)
	{
		//Verwendung
		List<int> ints = [];
		ints.Add(1);

		//Eigene Klasse mit Generic
		DataStore<int> zahlen = new DataStore<int>();
		zahlen.Add(1, 0);
		Console.WriteLine(zahlen.Get(0));
	}

	static void Test<T>(T obj)
	{
		Console.WriteLine(typeof(T));
		Console.WriteLine(nameof(T)); //Gibt einen String des Typens zurück (int -> "Int32")
		Console.WriteLine(default(T)); //Gibt den Standardwert zurück (struct -> 0, false, class -> null)

		T variable = obj;
		if (obj != null)
		{

		}
		//Viel mehr nicht möglich
	}
}

public class DataStore<T>
{
	private T[] _data;

	public List<T> Data => _data.ToList();

	public void Add(T item, int index)
	{
		_data[index] = item;
	}

	public T Get(int index)
	{
		return _data[index];
	}

	public DataStore()
	{
		_data = new T[10];
	}
}