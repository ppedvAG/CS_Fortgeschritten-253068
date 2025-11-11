namespace Multitasking;

internal class _09_ConcurrentCollection
{
	static void Main(string[] args)
	{
		//Sammlung von Collections, welche automatisch locken

		//Listenäquivalent
		SynchronizedCollection<int> collection = []; //System.ServiceModel.Primitives
		collection.Add(1); //Lockt intern automatisch
	}
}
