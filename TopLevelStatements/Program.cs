using System.Text;
using System.Text.Json;

Console.WriteLine(args);

Type je = typeof(JsonElement);
IEnumerable<string> select = je.GetMethods().Where(e => !e.Name.Contains("Try")).Select(e => $"{e.ReturnType.Name} => element.{e.Name}()");
StringBuilder sb = new("return element switch\n{\n");
foreach (string selectItem in select)
{
	sb.Append('\t');
	sb.Append(selectItem);
	sb.Append(",\n");
}
sb.Append("\t_ => throw new Exception(\"Unbekannter Typ\")\n};");
string switchPattern = sb.ToString();
Console.WriteLine(switchPattern);