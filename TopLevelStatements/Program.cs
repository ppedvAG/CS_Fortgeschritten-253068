Console.WriteLine(args);

IEnumerable<int> zahlen = Generiere();
//foreach (int i in zahlen)
//{
//	Console.WriteLine(i);
//	if (i > 2_000_000_000)
//		break;
//}

IEnumerator<int> enumerator = zahlen.GetEnumerator();
enumerator.MoveNext();
start:
Console.WriteLine(enumerator.Current);
if (enumerator.MoveNext())
	goto start;
enumerator.Reset();


IEnumerable<int> Generiere()
{
	while (true)
		yield return Random.Shared.Next();
}