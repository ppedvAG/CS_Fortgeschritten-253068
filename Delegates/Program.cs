internal class Program
{
	public delegate void Vorstellung(string name); //Eigenes Delegate

	private static void Main(string[] args)
	{
		//Delegates
		//Behälter für Methodenzeiger
		Vorstellung v = new Vorstellung(VorstellungDE); //Erstellung des Delegates mit einer Initialmethode
		v("Max"); //Führt alle angehängten Methoden aus

		//Mit += weitere Methoden anhängen
		v += VorstellungEN;
		v("Tom"); //Jetzt hängen an dem Delegate 2 Methoden daran -> 2 Outputs

		v += VorstellungEN;
		v += VorstellungEN;
		v += VorstellungEN;
		v += VorstellungEN;
		v("Udo"); //Die gleiche Methode kann auch mehrmals angehängt werden

		//Mit -= Methode herunternehmen
		v -= VorstellungDE;
		v("Max");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		//v("Tom"); //Wenn das Delegate leer ist, ist es null

		if (v != null)
			v("Tom");

		v?.Invoke("Tom"); //Null propagation

		//Alle Delegates durchgehen
		foreach (Delegate dg in v.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	public static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	public static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}