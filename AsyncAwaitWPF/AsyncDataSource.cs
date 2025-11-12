namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	/// <summary>
	/// Gibt Zahlen in einem unbestimmten Intervall zurück
	/// </summary>
	public async IAsyncEnumerable<int> GeneriereZahlen()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(500, 2000));
			yield return Random.Shared.Next();
		}
	}
}
