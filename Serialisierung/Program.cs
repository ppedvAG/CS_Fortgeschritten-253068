using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
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

		string filePath = "Fahrzeuge.xml";

		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			xml.Serialize(sw, fahrzeuge);
		}

		using (StreamReader sr = new StreamReader(filePath))
		{
			List<Fahrzeug> readFzg = (List<Fahrzeug>) xml.Deserialize(sr);
		}

		/////////////////////////////////////////////////////////////////////
		
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		foreach (XmlNode node in doc.DocumentElement)
		{
			int v = int.Parse(node.Attributes["MaxV"].Value);
			FahrzeugMarke fm = Enum.Parse<FahrzeugMarke>(node.Attributes["Marke"].Value);
			Console.WriteLine($"Fahrzeug: {v}km/h, {fm}");
		}
	}

	static void SystemJson()
	{
		//List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		//{
		//	new PKW(251, FahrzeugMarke.BMW),
		//	new Fahrzeug(274, FahrzeugMarke.BMW),
		//	new Fahrzeug(146, FahrzeugMarke.BMW),
		//	new Fahrzeug(208, FahrzeugMarke.Audi),
		//	new Fahrzeug(189, FahrzeugMarke.Audi),
		//	new Fahrzeug(133, FahrzeugMarke.VW),
		//	new Fahrzeug(253, FahrzeugMarke.VW),
		//	new Fahrzeug(304, FahrzeugMarke.BMW),
		//	new Fahrzeug(151, FahrzeugMarke.VW),
		//	new Fahrzeug(250, FahrzeugMarke.VW),
		//	new Fahrzeug(217, FahrzeugMarke.Audi),
		//	new Fahrzeug(125, FahrzeugMarke.Audi)
		//};

		//string filePath = "Fahrzeuge.json";

		////string json = JsonSerializer.Serialize(fahrzeuge);
		////File.WriteAllText(filePath, json);

		////string readFile = File.ReadAllText(filePath);
		////Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readFile);

		///////////////////////////////////////////////////////////////////////////////////

		//JsonSerializerOptions options = new JsonSerializerOptions();
		//options.WriteIndented = true;
		//options.Converters.Add(new JsonStringEnumConverter());

		//string json = JsonSerializer.Serialize(fahrzeuge, options); //WICHTIG: Options mitgeben
		//File.WriteAllText(filePath, json);

		//string readFile = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readFile, options);

		///////////////////////////////////////////////////////////////////////////////////

		//JsonDocument doc = JsonDocument.Parse(readFile);
		//foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	int v = element.GetProperty("MaxV").GetInt32();
		//	FahrzeugMarke fm = Enum.Parse<FahrzeugMarke>(element.GetProperty("Marke").GetString());
		//	Console.WriteLine($"Fahrzeug: {v}km/h, {fm}");
		//}
	}

	static void NewtonsoftJson()
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new PKW(251, FahrzeugMarke.BMW),
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

		string filePath = "Fahrzeuge.json";

		//string json = JsonConvert.SerializeObject(fahrzeuge);
		//File.WriteAllText(filePath, json);

		//string readFile = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readFile);

		/////////////////////////////////////////////////////////////////////////////////

		JsonSerializerSettings settings = new();
		settings.Formatting = Newtonsoft.Json.Formatting.Indented;
		settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung

		string json = JsonConvert.SerializeObject(fahrzeuge, settings); //WICHTIG: Settings mitgeben
		File.WriteAllText(filePath, json);

		string readFile = File.ReadAllText(filePath);
		Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readFile, settings);

		/////////////////////////////////////////////////////////////////////////////////

		//JsonDocument doc = JsonDocument.Parse(readFile);
		//foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	int v = element.GetProperty("MaxV").GetInt32();
		//	FahrzeugMarke fm = Enum.Parse<FahrzeugMarke>(element.GetProperty("Marke").GetString());
		//	Console.WriteLine($"Fahrzeug: {v}km/h, {fm}");
		//}

		JToken doc = JToken.Parse(readFile);
		foreach (JToken element in doc)
		{
			int v = element["MaxV"].Value<int>();
			FahrzeugMarke fm = (FahrzeugMarke) element["DieMarke"].Value<int>();
			Console.WriteLine($"Fahrzeug: {v}km/h, {fm}");
		}
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]
//[JsonDerivedType(typeof(Fahrzeug), "FZG")]
//[JsonDerivedType(typeof(PKW), "PKW")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	[XmlAttribute]
	public int MaxV { get; set; }

	[JsonProperty(PropertyName = "DieMarke", Order = 1)] //Allg. Attribut für viele Dinge
	[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	//[Newtonsoft.Json.JsonExtensionData] //Fängt Felder auf, welche in dieser Klasse kein entsprechendes Feld haben
	//public Dictionary<string, object> OtherData { get; set; }

	public Fahrzeug(int MaxV, FahrzeugMarke Marke)
	{
		this.MaxV = MaxV;
		this.Marke = Marke;
	}

	public Fahrzeug()
	{
		
	}
}

public class PKW : Fahrzeug
{
	public PKW(int MaxV, FahrzeugMarke Marke) : base(MaxV, Marke) { }

	public PKW()
	{
		
	}
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}