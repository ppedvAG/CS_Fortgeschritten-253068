namespace PluginBase;

[AttributeUsage(AttributeTargets.Method)]
public class ReflectionVisible : Attribute
{
	public string Name { get; private set; }

	public ReflectionVisible(string str)
	{
		Name = str;
	}
}